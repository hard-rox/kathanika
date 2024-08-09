import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AddAuthorInput, Author, AuthorPatchInput, GetAuthorGQL, UpdateAuthorGQL } from '@kathanika/graphql-ts-client';
import { MessageAlertService } from '../../../../core/services/message-alert/message-alert.service';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';
import { finalize } from 'rxjs';

@Component({
  templateUrl: './author-update.component.html'
})
export class AuthorUpdateComponent implements OnInit {
  @ViewChild('authorUpdateForm') authorUpdateForm:
    | AuthorFormComponent
    | undefined;

  constructor(
    private gql: GetAuthorGQL,
    private mutation: UpdateAuthorGQL,
    private alertService: MessageAlertService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) {}

  isPanelLoading = true;
  authorId: string | undefined;
  authorToUpdate!: Author;
  errors: string[] = [];

  ngOnInit(): void {
    this.authorId = this.activatedRoute.snapshot.params['id'];
    if (this.authorId && this.authorId.length > 0) {
      this.gql
        .fetch({
          id: this.authorId,
        })
        .pipe(finalize(() => {
          this.isPanelLoading = false;
        }))
        .subscribe({
          next: (result) => {
            // console.debug(result);
            if (result.error || result.errors) {
              this.alertService.showPopup(
                'error',
                result.error?.message ??
                  (result.errors?.join('<br/>') as string),
              );
            } else if (result.data.author == null) {
              this.alertService.showPopup(
                'error',
                'Returning to list page.',
                'Author not found',
              );
              this.router.navigate(['/authors']);
            } else {
              this.authorToUpdate = result.data.author;
            }
          },
          error: (err) => {
            this.alertService.showHttpErrorPopup(err);
          },
        });
    }
  }

  onValidFormSubmit(authorOutput: AddAuthorInput | AuthorPatchInput) {
    this.isPanelLoading = true;

    this.mutation
      .mutate({
        id: this.authorId as string,
        authorPatch: authorOutput,
      })
      .subscribe({
        next: (result) => {
          // console.debug(JSON.stringify(result));
          if (result.errors || result.data?.updateAuthor.errors) {
            this.errors = [];
            result.data?.updateAuthor.errors?.forEach((x) => {
              switch (x?.__typename) {
                case 'ValidationError':
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
              result.data?.updateAuthor.message ?? 'Author updated.',
            );
            this.authorUpdateForm?.resetForm();
            this.router.navigate([
              `/authors/${result.data?.updateAuthor.data?.id}`,
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
