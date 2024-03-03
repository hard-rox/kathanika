import { Component, ViewChild } from '@angular/core';
import { CreateMemberGQL } from '@kathanika/graphql-ts-client';
import { MemberFormComponent } from '../../components/member-form/member-form.component';
import { MessageAlertService } from "../../../../core/services/message-alert.service";
import { MemberFormOutput } from '../../types/member-form-output';
import { Router } from '@angular/router';

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

  onValidFormSubmit(formValue: MemberFormOutput) {
    this.isPanelLoading = true;
    this.gql.mutate({ createMemberInput: formValue }).subscribe({
      next: (result) => {
        // console.debug(result);
        if (result.errors || result.data?.createMember.errors) {
          this.errors = [];
          result.data?.createMember.errors?.forEach((x) =>
            this.errors.push(`${x.fieldName} - ${x.message}`),
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
        this.isPanelLoading = false;
      },
    });
  }

  closeAlert() {
    this.errors = [];
  }
}
