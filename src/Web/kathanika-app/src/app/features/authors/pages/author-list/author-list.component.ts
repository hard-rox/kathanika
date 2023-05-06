import { Component, OnInit } from '@angular/core';
import { ApolloQueryResult } from '@apollo/client/core';
import { QueryRef } from 'apollo-angular';
import { Observable } from 'rxjs';
import {
  GetAuthorsGQL,
  GetAuthorsQuery,
  SortEnumType,
} from 'src/app/graphql/generated';

@Component({
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.scss'],
})
export class AuthorListComponent implements OnInit {
  constructor(private getAuthorsGql: GetAuthorsGQL) {}

  page: number = 1;
  authorsQueryRef: QueryRef<GetAuthorsQuery, any> = this.getAuthorsGql.watch({
    skip: (this.page - 1) * 3,
    take: 3,
    sortBy: {
      firstName: SortEnumType.Asc,
    },
    filter: {
      firstName: {
        ncontains: '/'
      }
    }
  });
  authorsQuery: Observable<ApolloQueryResult<GetAuthorsQuery>> =
    this.authorsQueryRef.valueChanges;

  ngOnInit(): void {}

  changePage() {
    this.authorsQueryRef.refetch({ skip: (this.page - 1) * 3, take: 3 });
  }
}
