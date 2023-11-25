import { Component, OnInit, ViewChild } from '@angular/core';
import { PublisherFormComponent } from '../../components/publisher-form/publisher-form.component';
import { GetPublisherGQL, UpdatePublisherGQL } from '@kathanika/graphql-consumer';
import { MessageAlertService } from '../../../../core/services/message-alert.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PublisherFormInput } from '../../types/publisher-form-input';
import { PublisherFormOutput } from '../../types/publisher-form-output';

@Component({
  templateUrl: './publisher-update.component.html',
  styleUrls: ['./publisher-update.component.scss']
})
export class PublisherUpdateComponent implements OnInit {
  @ViewChild('publisherUpdateForm') publisherUpdateForm:
    | PublisherFormComponent
    | undefined;

  constructor(
    private gql: GetPublisherGQL,
    private mutation: UpdatePublisherGQL,
    private alertService: MessageAlertService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) { }

  isPanelLoading: boolean = true;
  publisherId: string | undefined;
  publisherFormInput: PublisherFormInput | null | undefined;
  errors: string[] = [];

  ngOnInit(): void {
    this.publisherId = this.activatedRoute.snapshot.params['id'];
    if (this.publisherId && this.publisherId.length > 0) {
      this.gql
        .fetch({
          id: this.publisherId,
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
            } else if (result.data.publisher == null) {
              this.alertService.showPopup(
                'error',
                'Returning to list page.',
                'Publisher not found'
              );
              this.router.navigate(['/publishers']);
            } else {
              this.publisherFormInput = {
                name: result.data.publisher.name,
                description: result.data.publisher.description,
                contactInformation: result.data.publisher.contactInformation
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

  onValidFormSubmit(publisherOutput: PublisherFormOutput) {
    this.isPanelLoading = true;

    this.mutation
      .mutate({
        id: this.publisherId as string,
        publisherPatch: publisherOutput,
      })
      .subscribe({
        next: (result) => {
          // console.debug(JSON.stringify(result));
          if (result.errors || result.data?.updatePublisher.errors) {
            this.errors = [];
            result.data?.updatePublisher.errors?.forEach((x) => {
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
              result.data?.updatePublisher.message ?? 'Publisher updated.'
            );
            this.publisherUpdateForm?.resetForm();
            this.router.navigate([
              `/publishers`,
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
