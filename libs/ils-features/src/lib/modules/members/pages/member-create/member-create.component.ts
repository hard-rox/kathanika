import { Component, ViewChild } from '@angular/core';
import { CreateMemberGQL, CreateMemberInput, MemberPatchInput } from '@kathanika/graphql-ts-client';
import { MemberFormComponent } from '../../components/member-form/member-form.component';
import { MessageAlertService } from "../../../../core/services/message-alert/message-alert.service";
import { Router } from '@angular/router';
import { finalize } from 'rxjs';

@Component({
  templateUrl: './member-create.component.html'
})
export class MemberCreateComponent {
  @ViewChild('memberCreateForm') memberCreateForm: MemberFormComponent | undefined;

  constructor(
    private gql: CreateMemberGQL,
    private alertService: MessageAlertService,
    private router: Router,
  ) {}

  isPanelLoading = false;
  errors: string[] = [];

  onValidFormSubmit(formValue: CreateMemberInput | MemberPatchInput) {
    this.isPanelLoading = true;
    this.gql.mutate({ createMemberInput: formValue as CreateMemberInput })
      .pipe(finalize(() => {
        this.isPanelLoading = false;
      }))
      .subscribe({
      next: (result) => {
        // console.debug(result);
        if (result.errors || result.data?.createMember.errors) {
          this.errors = [];
          result.data?.createMember.errors?.forEach((x) =>{
            switch (x?.__typename) {
              case 'ValidationError':
                this.errors.push(`${x.fieldName} - ${x.message}`);
                break;
              default:
                this.errors.push(x.message);
                break;
            }
          }
          );
          result.errors?.forEach((x) => this.errors.push(x.message));
        } else {
          this.alertService.showToast(
            'success',
            result.data?.createMember.message ?? 'Member added.',
          );
          this.memberCreateForm?.resetForm();
          this.router.navigate([`/members/${result.data?.createMember.data?.id}`]);
        }
      },
        error: (err) => {
          this.alertService.showHttpErrorPopup(err);
      }
    });
  }

  closeAlert() {
    this.errors = [];
  }
}
