import { Component, OnInit, ViewChild } from '@angular/core';
import { MemberFormComponent } from '../../components/member-form/member-form.component';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateMemberInput, GetMemberGQL, Member, MemberPatchInput, UpdateMemberGQL } from '@kathanika/graphql-ts-client';
import { MessageAlertService } from "../../../../core/services/message-alert/message-alert.service";
import { finalize } from 'rxjs';


@Component({
  templateUrl: './member-update.component.html'
})
export class MemberUpdateComponent implements OnInit {
  @ViewChild('memberUpdateForm') memberUpdateForm!: MemberFormComponent;

  constructor(
    private gql: GetMemberGQL,
    private mutation: UpdateMemberGQL,
    private alertService: MessageAlertService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) { }

  isPanelLoading = true;
  memberId: string | undefined;
  memberToUpdate: Member | null = null;
  errors: string[] = [];

  ngOnInit(): void {
    this.memberId = this.activatedRoute.snapshot.params['id'];
    if (this.memberId && this.memberId.length > 0) {
      this.gql
        .fetch({
          id: this.memberId,
        })
        .pipe(finalize(() => {
          this.isPanelLoading = false;
        }))
        .subscribe({
          next: (result) => {
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
              this.memberToUpdate = { ...result.data.member };
            }
          },
          error: (err) => {
            this.alertService.showHttpErrorPopup(err);
          },
        });
    }
  }

  onValidFormSubmit(memberOutput: CreateMemberInput | MemberPatchInput) {
    this.isPanelLoading = true;

    this.mutation
      .mutate({
        id: this.memberId as string,
        memberPatch: memberOutput,
      })
      .pipe(finalize(() => {
        this.isPanelLoading = false;
      }))
      .subscribe({
        next: (result) => {
          // console.debug(JSON.stringify(result));
          if (result.errors || result.data?.updateMember.errors) {
            this.errors = [];
            result.data?.updateMember.errors?.forEach((x) => {
              switch (x.__typename) {
                case 'ValidationError':
                  this.errors.push(`${x.fieldName} - ${x.message}`);
                  break;
                default:
                  this.errors.push(x.message);
                  break;
              }
            });
            result.errors?.forEach((x) => this.errors.push(x.message));
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
        },
        error: (err) => {
          this.alertService.showHttpErrorPopup(err)
        },
      });
  }

  closeAlert() {
    this.errors = [];
  }
}
