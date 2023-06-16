import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  GetAuthorQuery,
  GetAuthorQueryVariables,
  GetAuthorGQL,
  UpadateAuthorGQL,
} from 'src/app/graphql/generated/graphql-operations';
import { BaseQueryComponent } from 'src/app/shared/bases/base-query-component';
import { AuthorFormInput } from '../../types/author-form-input';
import { AuthorFormOutput } from '../../types/author-form-output';
import { MessageAlertService } from 'src/app/shared/services/message-alert.service';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';

@Component({
  templateUrl: './author-update.component.html',
  styleUrls: ['./author-update.component.scss'],
})
export class AuthorUpdateComponent
  extends BaseQueryComponent<GetAuthorQuery, GetAuthorQueryVariables>
  implements OnInit
{
  @ViewChild('authorUpdateForm') authorUpdateForm:
    | AuthorFormComponent
    | undefined;

  constructor(
    gql: GetAuthorGQL,
    private mutation: UpadateAuthorGQL,
    private alertService: MessageAlertService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    super(gql);
  }

  authorId: string | undefined;
  authorFormInput: AuthorFormInput | null | undefined;
  errors: string[] = [];

  ngOnInit(): void {
    this.authorId = this.activatedRoute.snapshot.params['id'];
    if (this.authorId && this.authorId.length > 0) {
      this.queryVariables = {
        id: this.authorId,
      };
      this.queryRef.refetch(this.queryVariables);
      this.queryRef.valueChanges.subscribe({
        next: (result) => {
          console.debug(result);
          this.authorFormInput = result.data.author;
        },
      });
    }
  }

  onValidFormSubmit(authorOutput: AuthorFormOutput) {
    this.mutation
      .mutate({
        id: this.authorId ?? '',
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
          if (result.errors) {
            result.errors.forEach((x) => this.errors.push(x.message));
          } else {
            this.alertService.showSuccess(
              result.data?.updateAuthor.message ?? 'Author updated.',
              'Author updated'
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
