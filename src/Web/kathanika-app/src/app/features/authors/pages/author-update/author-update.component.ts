import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  GetAuthorGQL,
  UpadateAuthorGQL,
} from 'src/app/graphql/generated/graphql-operations';
import { AuthorFormInput } from '../../types/author-form-input';
import { AuthorFormOutput } from '../../types/author-form-output';
import { MessageAlertService } from 'src/app/core/services/message-alert.service';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';

@Component({
  templateUrl: './author-update.component.html',
  styleUrls: ['./author-update.component.scss'],
})
export class AuthorUpdateComponent implements OnInit {
  @ViewChild('authorUpdateForm') authorUpdateForm:
    | AuthorFormComponent
    | undefined;

  constructor(
    private gql: GetAuthorGQL,
    private mutation: UpadateAuthorGQL,
    private alertService: MessageAlertService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  authorId: string | undefined;
  authorFormInput: AuthorFormInput | null | undefined;
  errors: string[] = [];

  ngOnInit(): void {
    this.authorId = this.activatedRoute.snapshot.params['id'];
    if (this.authorId && this.authorId.length > 0) {
      this.gql
        .fetch({
          id: this.authorId,
        })
        .subscribe({
          next: (result) => {
            // console.debug(result);
            this.authorFormInput = result.data.author;
          },
        });
    }
  }

  onValidFormSubmit(authorOutput: AuthorFormOutput) {
    this.mutation
      .mutate({
        id: this.authorId as string,
        authorPatch: {
          firstName: authorOutput.firstName,
          lastName: authorOutput.lastName,
          dateOfBirth: authorOutput.dateOfBirth,
          nationality: authorOutput.nationality,
          biography: authorOutput.biography,
        },
      })
      .subscribe({
        next: (result) => {
          console.log(result);
          if (result.errors) {
            result.errors.forEach((x) => this.errors.push(x.message));
          } else {
            this.alertService.showToast(
              'success',
              result.data?.updateAuthor.message ?? 'Author updated.'
            );
            this.authorUpdateForm?.resetForm();
            this.router.navigate([
              `/authors/${result.data?.updateAuthor.data?.id}`,
            ]);
          }
        },
      });
  }
}
