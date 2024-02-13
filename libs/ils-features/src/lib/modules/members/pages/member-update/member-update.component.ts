import { Component, OnInit, ViewChild } from '@angular/core';
import { MemberFormComponent } from '../../components/member-form/member-form.component';
import { ActivatedRoute, Router } from '@angular/router';
import { GetMemberGQL, UpdateMemberGQL } from '@kathanika/graphql-ts-client';
import { MemberFormInput } from '../../types/member-form-input';
import { MemberFormOutput } from '../../types/member-form-output';
import { MessageAlertService } from "../../../../core/services/message-alert.service";


@Component({
  templateUrl: './member-update.component.html',
  styleUrls: ['./member-update.component.scss'],
})
export class MemberUpdateComponent  implements OnInit {
  @ViewChild('memberUpdateForm') memberUpdateForm:
    | MemberFormComponent
    | undefined;

  constructor(
    private gql: GetMemberGQL,
    private mutation: UpdateMemberGQL,
    private alertService: MessageAlertService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {}

  isPanelLoading = true;
  memberId: string | undefined;
  memberFormInput!: MemberFormInput;
  errors: string[] = [];

  ngOnInit(): void {
    this.memberId = this.activatedRoute.snapshot.params['id'];
    if (this.memberId && this.memberId.length > 0) {
      this.gql
        .fetch({
          id: this.memberId,
        })
        .subscribe({
          next: (result) => {
            // console.debug(result);
            if (result.error || result.errors) {
              this.alertService.showPopup(
                'error',
                result.error?.message ??
                  (result.errors?.join('<br/>') as string),
              );
            } else if (result.data.member == null) {
              this.alertService.showPopup(
                'error',
                'Returning to list page.',
                'Member not found',
              );
              this.router.navigate(['/members']);
            } else {
              this.memberFormInput = { ...result.data.member };
              this.isPanelLoading = false;
            }
          },
          error: (err) => {
            // console.debug(JSON.stringify(err));
            this.alertService.showPopup('error', err.message);
          },
        });
    }
  }

  onValidFormSubmit(memberOutput: MemberFormOutput) {
    this.isPanelLoading = true;

    this.mutation
      .mutate({
        id: this.memberId as string,
        memberPatch: memberOutput,
      })
      .subscribe({
        next: (result) => {
          // console.debug(JSON.stringify(result));
          if (result.errors || result.data?.updateMember.errors) {
            this.errors = [];
            result.data?.updateMember.errors?.forEach((x) => {
              switch (x.__typename) {
                case 'InvalidFieldError':
                  this.errors.push(`${x.fieldName} - ${x.message}`);
                  break;
                default:
                  this.errors.push(x.message);
                  break;
              }
            });
            result.errors?.forEach((x) => this.errors.push(x.message));
            this.isPanelLoading = false;
          } else {
            this.alertService.showToast(
              'success',
              result.data?.updateMember.message ?? 'Member updated.',
            );
            this.memberUpdateForm?.resetForm();
            this.router.navigate([
              `/members/${result.data?.updateMember.data?.id}`,
            ]);
          }
          this.isPanelLoading = false;
        },
        error: () => {
          this.errors.push('Something wrong happened.');
          this.isPanelLoading = false;
        },
      });
  }

  closeAlert() {
    this.errors = [];
  }
}
