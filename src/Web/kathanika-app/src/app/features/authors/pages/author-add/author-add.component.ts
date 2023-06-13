import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AddAuthorGQL } from 'src/app/graphql/generated/graphql-operations';
import { AddAuthorInput } from 'src/app/graphql/generated/graphql-operations';
import { MessageAlertService } from 'src/app/shared/services/message-alert.service';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';

@Component({
  templateUrl: './author-add.component.html',
  styleUrls: ['./author-add.component.scss'],
})
export class AuthorAddComponent {

  @ViewChild('authorAddForm') authorAddForm: AuthorFormComponent | undefined;

  constructor(
    private gql: AddAuthorGQL,
    private alertService: MessageAlertService,
    private router: Router
  ) {}

  errors: string[] = [];

  onValidFormSubmit(formValue: AddAuthorInput) {
    this.gql.mutate({ addAuthorInput: formValue }).subscribe({
      next: (result) => {
        if (result.errors) {
          result.errors.forEach((x) => this.errors.push(x.message));
        } else {
          this.alertService.showSuccess(
            result.data?.addAuthor.message ?? 'Author added.',
            'Author added'
          );
          this.authorAddForm?.resetForm();
          this.router.navigate([`/authors/${result.data?.addAuthor.data?.id}`]);
        }
      },
    });
  }
}
