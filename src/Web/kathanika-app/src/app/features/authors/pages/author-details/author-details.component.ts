import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApolloQueryResult } from '@apollo/client/core';
import { QueryRef } from 'apollo-angular';
import { Observable } from 'rxjs';
import {
  GetAuthorGQL,
  GetAuthorQuery,
  GetAuthorQueryVariables,
} from 'src/app/graphql/generated/graphql-operations';

@Component({
  templateUrl: './author-details.component.html',
  styleUrls: ['./author-details.component.scss'],
})
export class AuthorDetailsComponent implements OnInit {
  private _queryVariable!: GetAuthorQueryVariables;

  private _queryRef: QueryRef<GetAuthorQuery, GetAuthorQueryVariables> =
    this.getAuthorGql.watch(this._queryVariable);

  constructor(
    private getAuthorGql: GetAuthorGQL,
    private activatedRoute: ActivatedRoute
  ) {}

  query: Observable<ApolloQueryResult<GetAuthorQuery>> =
    this._queryRef.valueChanges;

  ngOnInit(): void {
    const authorId = this.activatedRoute.snapshot.params['id'];
    if (authorId && authorId.length > 0) {
      this._queryVariable = {
        id: authorId,
      };
      this._queryRef.refetch(this._queryVariable);
    }
  }
}
