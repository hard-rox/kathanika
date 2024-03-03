import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  GetAuthorGQL,
  GetAuthorQuery,
  GetAuthorQueryVariables,
} from '@kathanika/graphql-ts-client';
import { BaseQueryComponent } from '../../../../abstractions/base-query-component';

@Component({
  templateUrl: './author-details.component.html'
})
export class AuthorDetailsComponent
  extends BaseQueryComponent<GetAuthorQuery, GetAuthorQueryVariables>
  implements OnInit
{
  constructor(
    gql: GetAuthorGQL,
    private activatedRoute: ActivatedRoute,
  ) {
    super(gql);
  }

  ngOnInit(): void {
    const authorId = this.activatedRoute.snapshot.params['id'];
    if (authorId && authorId.length > 0) {
      this.queryVariables = {
        id: authorId,
      };
      this.queryRef.refetch(this.queryVariables);
    }
  }
}
