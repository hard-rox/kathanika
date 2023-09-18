import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  GetPublicationGQL,
  GetPublicationQuery,
  GetPublicationQueryVariables,
} from 'src/app/graphql/generated/graphql-operations';
import { BaseQueryComponent } from 'src/app/shared/bases/base-query-component';

@Component({
  templateUrl: './publication-details.component.html',
  styleUrls: ['./publication-details.component.scss'],
})
export class PublicationDetailsComponent
  extends BaseQueryComponent<GetPublicationQuery, GetPublicationQueryVariables>
  implements OnInit
{
  constructor(gql: GetPublicationGQL, private activatedRoute: ActivatedRoute) {
    super(gql);
  }

  ngOnInit(): void {
    const publicationId = this.activatedRoute.snapshot.params['id'];
    if (publicationId && publicationId.length > 0) {
      this.queryVariables = {
        id: publicationId,
      };
      this.queryRef.refetch(this.queryVariables);
    }
  }
}
