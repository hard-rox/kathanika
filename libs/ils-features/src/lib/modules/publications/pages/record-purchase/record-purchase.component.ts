import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { GetPublicationGQL } from '@kathanika/graphql-ts-client';
import { BaseFormComponent, FormGroupModel } from '../../../../abstractions/base-form-component';

@Component({
  templateUrl: './record-purchase.component.html'
})
export class RecordPurchaseComponent
  extends BaseFormComponent<any>
  implements OnInit {

  protected override createFormGroup(): FormGroupModel<any> {
    const group = new FormGroup({

    });
    return group;
  }

  constructor(
    private activatedRoute: ActivatedRoute,
    private getPublicationGql: GetPublicationGQL
  ) {
    super();
  }

  ngOnInit(): void {
    const publicationId = this.activatedRoute.snapshot.queryParams['id'];
    this.getPublicationGql.fetch({ id: publicationId })
      .subscribe({
        next: (value) => {
          this.formGroup.patchValue({
            publicationId: publicationId,
            title: value.data.publication?.title
          });
        }
      })
  }
}
