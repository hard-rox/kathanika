import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  GetAuthorGQL,
  GetAuthorQuery,
  GetAuthorQueryVariables,
} from 'src/app/graphql/generated/graphql-operations';
import { BaseQueryComponent } from 'src/app/shared/bases/base-query-component';

@Component({
  templateUrl: './author-details.component.html',
  styleUrls: ['./author-details.component.scss'],
})
export class AuthorDetailsComponent
  extends BaseQueryComponent<GetAuthorQuery, GetAuthorQueryVariables>
  implements OnInit
{
  constructor(gql: GetAuthorGQL,
    private activatedRoute: ActivatedRoute) {
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
