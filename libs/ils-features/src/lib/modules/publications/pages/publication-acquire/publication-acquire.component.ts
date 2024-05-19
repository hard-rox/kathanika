import { Component, ViewChild } from '@angular/core';
import { MessageAlertService } from '../../../../core/services/message-alert/message-alert.service';
import { Router } from '@angular/router';
import { AcquirePublicationFormComponent } from '../../components/acquire-publication-form/acquire-publication-form.component';
import { AcquirePublicationGQL, AcquirePublicationInput } from '@kathanika/graphql-ts-client';

@Component({
  templateUrl: './publication-acquire.component.html'
})
export class PublicationAcquireComponent {
  @ViewChild('acquirePublicationForm') acquirePublicationForm:
    | AcquirePublicationFormComponent
    | undefined;

  constructor(
    private gql: AcquirePublicationGQL,
    private alertService: MessageAlertService,
    private router: Router,
  ) {}

  isPanelLoading = false;
  errors: string[] = [];

  onValidFormSubmit(formValue: AcquirePublicationInput) {
    this.isPanelLoading = true;
    this.gql
      .mutate({
        acquirePublicationInput: formValue,
      })
      .subscribe({
        next: (result) => {
          if (result.errors || result.data?.acquirePublication.errors) {
            this.errors = [];
            result.data?.acquirePublication.errors?.forEach((x) =>
              this.errors.push(`${x.fieldName} - ${x.message}`),
            );
            result.errors?.forEach((x) => this.errors.push(x.message));
          } else {
            this.alertService.showToast(
              'success',
              result.data?.acquirePublication.message ?? 'Publication added.',
            );
            this.acquirePublicationForm?.resetForm();
            this.router.navigate([
              `/publications/${result.data?.acquirePublication.data?.id}`,
            ]);
          }
          this.isPanelLoading = false;
        },
        error: (err) => {
          // console.debug(JSON.stringify(err));
          this.alertService.showPopup('error', err.message);
        }
      });
  }

  closeAlert() {
    this.errors = [];
  }
}
