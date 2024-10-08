import { Component, ViewChild } from '@angular/core';
import { PublisherFormComponent } from '../../components/publisher-form/publisher-form.component';
import {
  AddPublisherGQL,
  AddPublisherInput,
} from '@kathanika/graphql-ts-client';
import { MessageAlertService } from '../../../../core/services/message-alert/message-alert.service';
import { Router } from '@angular/router';

@Component({
  templateUrl: './publisher-add.component.html'
})
export class PublisherAddComponent {
  @ViewChild('publisherAddForm') publisherAddForm:
    | PublisherFormComponent
    | undefined;

  constructor(
    private gql: AddPublisherGQL,
    private alertService: MessageAlertService,
    private router: Router,
  ) {}

  isPanelLoading = false;
  errors: string[] = [];

  onValidFormSubmit(formValue: AddPublisherInput) {
    this.isPanelLoading = true;
    this.gql.mutate({ addPublisherInput: formValue }).subscribe({
      next: (result) => {
        if (result.errors || result.data?.addPublisher.errors) {
          this.errors = [];
          result.data?.addPublisher.errors?.forEach((x) =>{
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
            result.data?.addPublisher.message ?? 'Publisher added.',
          );
          this.publisherAddForm?.resetForm();
          this.router.navigate([`/publishers`]);
        }
        this.isPanelLoading = false;
      },
    });
  }

  closeAlert() {
    this.errors = [];
  }
}
