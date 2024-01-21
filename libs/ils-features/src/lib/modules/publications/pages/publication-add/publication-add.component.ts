import { Component, ViewChild } from '@angular/core';
import { PublicationFormComponent } from '../../components/publication-form/publication-form.component';
import { MessageAlertService } from '../../../../core/services/message-alert.service';
import { Router } from '@angular/router';
import { AddPublicationGQL } from '@kathanika/graphql-ts-client';
import { PublicationFormOutput } from '../../types/publication-form-output';

@Component({
  templateUrl: './publication-add.component.html',
  styleUrls: ['./publication-add.component.scss'],
})
export class PublicationAddComponent {
  @ViewChild('publicationAddForm') publicationAddForm:
    | PublicationFormComponent
    | undefined;

  constructor(
    private gql: AddPublicationGQL,
    private alertService: MessageAlertService,
    private router: Router,
  ) {}

  isPanelLoading: boolean = false;
  errors: string[] = [];

  onValidFormSubmit(formValue: PublicationFormOutput) {
    // console.debug(formValue);
    this.isPanelLoading = true;
    this.gql
      .mutate({
        addPublicationInput: {
          ...formValue,
          authorIds: formValue.authorIds ?? [], ///TODO: Fix to typed...
          isbn: formValue.isbn ?? '',
        },
      })
      .subscribe({
        next: (result) => {
          console.debug(result);
          if (result.errors || result.data?.addPublication.errors) {
            this.errors = [];
            result.data?.addPublication.errors?.forEach((x) =>
              this.errors.push(`${x.fieldName} - ${x.message}`),
            );
            result.errors?.forEach((x) => this.errors.push(x.message));
          } else {
            this.alertService.showToast(
              'success',
              result.data?.addPublication.message ?? 'Publication added.',
            );
            this.publicationAddForm?.resetForm();
            this.router.navigate([
              `/publications/${result.data?.addPublication.data?.id}`,
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
