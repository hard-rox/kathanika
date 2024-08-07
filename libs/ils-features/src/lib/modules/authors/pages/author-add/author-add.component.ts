import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AddAuthorGQL, AddAuthorInput, AuthorPatchInput } from '@kathanika/graphql-ts-client';
import { MessageAlertService } from '../../../../core/services/message-alert/message-alert.service';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';

@Component({
  templateUrl: './author-add.component.html'
})
export class AuthorAddComponent {
  @ViewChild('authorAddForm') authorAddForm: AuthorFormComponent | undefined;

  constructor(
    private gql: AddAuthorGQL,
    private alertService: MessageAlertService,
    private router: Router,
  ) {}

  isPanelLoading = false;
  errors: string[] = [];

  onValidFormSubmit(formValue: AddAuthorInput | AuthorPatchInput) {
    this.isPanelLoading = true;
    this.gql.mutate({ addAuthorInput: formValue as AddAuthorInput }).subscribe({
      next: (result) => {
        console.debug(result);
        if (result.errors || result.data?.addAuthor.errors) {
          this.errors = [];
          result.data?.addAuthor.errors?.forEach((x) =>
            this.errors.push(`${x?.message}`),
          );
          result.errors?.forEach((x) => this.errors.push(x.message));
        } else {
          this.alertService.showToast(
            'success',
            result.data?.addAuthor.message ?? 'Author added.',
          );
          this.authorAddForm?.resetForm();
          this.router.navigate([`/authors/${result.data?.addAuthor.data?.id}`]);
        }
        this.isPanelLoading = false;
      },
      error: (error) => {
        console.log(error);
      }
    });
  }

  closeAlert() {
    this.errors = [];
  }
}
