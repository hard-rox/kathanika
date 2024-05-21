import { Component, OnInit, ViewChild } from '@angular/core';
import { PublisherFormComponent } from '../../components/publisher-form/publisher-form.component';
import {
  GetPublisherGQL,
  Publisher,
  PublisherPatchInput,
  UpdatePublisherGQL,
} from '@kathanika/graphql-ts-client';
import { MessageAlertService } from '../../../../core/services/message-alert/message-alert.service';
import { ActivatedRoute, Router } from '@angular/router';
import { finalize } from 'rxjs';

@Component({
  templateUrl: './publisher-update.component.html'
})
export class PublisherUpdateComponent implements OnInit {
  @ViewChild('publisherUpdateForm') publisherUpdateForm!: PublisherFormComponent;

  constructor(
    private gql: GetPublisherGQL,
    private mutation: UpdatePublisherGQL,
    private alertService: MessageAlertService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
  ) { }

  isPanelLoading = true;
  publisherId!: string;
  publisherToUpdate!: Publisher;
  errors: string[] = [];

  ngOnInit(): void {
    this.publisherId = this.activatedRoute.snapshot.params['id'];
    if (!this.publisherId || this.publisherId.length == 0) {
      this.router.navigate(['/publishers']);
      return;
    }
    this.gql
      .fetch({
        id: this.publisherId,
      })
      .pipe(finalize(() => {
        this.isPanelLoading = false;
      }))
      .subscribe({
        next: (result) => {
          if (result.error || result.errors) {
            this.alertService.showPopup(
              'error',
              result.error?.message ??
              (result.errors?.join('<br/>') as string),
            );
          } else if (result.data.publisher == null) {
            this.alertService.showPopup(
              'error',
              'Returning to list page.',
              'Publisher not found',
            );
            this.router.navigate(['/publishers']);
          } else {
            this.publisherToUpdate = result.data.publisher;
          }
        },
        error: (err) => {
          this.alertService.showHttpErrorPopup(err);
        },
      });
  }

  onValidFormSubmit(publisherPatch: PublisherPatchInput) {
    this.isPanelLoading = true;

    this.mutation
      .mutate({
        id: this.publisherId as string,
        publisherPatch: publisherPatch,
      })
      .pipe(finalize(() => {
        this.isPanelLoading = false;
      }))
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
              result.data?.updatePublisher.message ?? 'Publisher updated.',
            );
            this.publisherUpdateForm?.resetForm();
            this.router.navigate([`/publishers/${result.data?.updatePublisher.data?.id}`]);
          }
        },
        error: (err) => {
          this.alertService.showHttpErrorPopup(err)
        }
      });
  }

  closeAlert() {
    this.errors = [];
  }
}
