import { Component, ViewChild } from '@angular/core';
import { PublicationFormComponent } from '../../components/publication-form/publication-form.component';
import { GetPublicationGQL, PublicationPatchInput, UpdatePublicationGQL } from 'src/app/graphql/generated/graphql-operations';
import { MessageAlertService } from 'src/app/core/services/message-alert.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PublicationFormInput } from '../../types/publication-form-input';
import { PublicationFormOutput } from '../../types/publication-form-output';

@Component({
  templateUrl: './publication-update.component.html',
  styleUrls: ['./publication-update.component.scss']
})
export class PublicationUpdateComponent {
  @ViewChild('publicationUpdateForm') publicationUpdateForm:
    | PublicationFormComponent
    | undefined;

  constructor(
    private gql: GetPublicationGQL,
    private mutation: UpdatePublicationGQL,
    private alertService: MessageAlertService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  isPanelLoading: boolean = true;
  publicationId: string | undefined;
  publicationFormInput: PublicationFormInput | null | undefined;
  errors: string[] = [];

  ngOnInit(): void {
    this.publicationId = this.activatedRoute.snapshot.params['id'];
    if (this.publicationId && this.publicationId.length > 0) {
      this.gql
        .fetch({
          id: this.publicationId,
        })
        .subscribe({
          next: (result) => {
            // console.debug(result);
            if (result.error || result.errors) {
              this.alertService.showPopup(
                'error',
                result.error?.message ??
                (result.errors?.join('<br/>') as string)
              );
            } else if (result.data.publication == null) {
              this.alertService.showPopup(
                'error',
                'Returning to list page.',
                'Publication not found'
              );
              this.router.navigate(['/publications']);
            } else {
              this.publicationFormInput = {
                ...result.data.publication,
                authors: result.data.publication.authors.map(x => x.id)
              };
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

  onValidFormSubmit(publicationOutput: PublicationFormOutput) {
    this.isPanelLoading = true;
    const publicationPatch: PublicationPatchInput = {
      ...publicationOutput
    };

    this.mutation
      .mutate({
        id: this.publicationId as string,
        publicationPatch: publicationPatch,
      })
      .subscribe({
        next: (result) => {
          // console.debug(JSON.stringify(result));
          if (result.errors || result.data?.updatePublication.errors) {
            this.errors = [];
            result.data?.updatePublication.errors?.forEach((x) => {
              switch (x.__typename) {
                case 'InvalidFieldError':
                  this.errors.push(`${x.fieldName} - ${x.message}`);
                  break;
                case 'NotFoundWithTheIdError':
                  this.errors.push(`${x.objectName} - ${x.message}`);
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
              result.data?.updatePublication.message ?? 'Publication updated.'
            );
            this.publicationUpdateForm?.resetForm();
            this.router.navigate([
              `/publications/${result.data?.updatePublication.data?.id}`,
            ]);
          }
          this.isPanelLoading = false;
        },
      });
  }

  closeAlert() {
    this.errors = [];
  }
}
