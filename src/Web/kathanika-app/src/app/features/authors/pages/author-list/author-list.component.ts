import { Component, OnInit } from '@angular/core';
import { ApolloQueryResult } from '@apollo/client/core';
import { QueryRef } from 'apollo-angular';
import { Observable } from 'rxjs';
import {
  GetAuthorsGQL,
  GetAuthorsQuery,
  GetAuthorsQueryVariables,
  SortEnumType,
} from 'src/app/graphql/generated';

@Component({
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.scss'],
})
export class AuthorListComponent implements OnInit {
  constructor(private getAuthorsGql: GetAuthorsGQL) {}

  page: number = 1;
  queryVariables: GetAuthorsQueryVariables = {
    skip: 0,
    take: 9,
    sortBy: {
      firstName: SortEnumType.Asc,
    },
  };
  authorsQueryRef: QueryRef<GetAuthorsQuery, GetAuthorsQueryVariables> =
    this.getAuthorsGql.watch(this.queryVariables);
  authorsQuery: Observable<ApolloQueryResult<GetAuthorsQuery>> =
    this.authorsQueryRef.valueChanges;

  ngOnInit(): void {}
}
