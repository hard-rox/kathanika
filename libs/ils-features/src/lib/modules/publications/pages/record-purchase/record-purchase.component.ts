import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GetPublicationGQL } from '@kathanika/graphql-ts-client';

@Component({
  templateUrl: './record-purchase.component.html'
})
export class RecordPurchaseComponent
  implements OnInit {

  constructor(
    private activatedRoute: ActivatedRoute,
    private getPublicationGql: GetPublicationGQL
  ) {
  }

  ngOnInit(): void {
    const publicationId = this.activatedRoute.snapshot.queryParams['id'];
    this.getPublicationGql.fetch({ id: publicationId })
      .subscribe({
        next: (value) => {
          console.debug(value);
        }
      })
  }
}
