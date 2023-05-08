import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
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
export class AuthorListComponent {
  private _queryVariables: GetAuthorsQueryVariables = {
    skip: 0,
    take: 2,
    sortBy: {
      lastName: SortEnumType.Asc,
    },
  };
  private _authorsQueryRef: QueryRef<
    GetAuthorsQuery,
    GetAuthorsQueryVariables
  > = this.getAuthorsGql.watch(this._queryVariables);

  constructor(
    private getAuthorsGql: GetAuthorsGQL,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    activatedRoute.queryParams.subscribe({
      next: (params) => {
        console.debug(params);
        this.router.navigate([], {
          relativeTo: this.activatedRoute,
          queryParams: {
            page: 1,
            size: this._queryVariables.take,
            sortBy: JSON.stringify(this._queryVariables.sortBy)
          },
          queryParamsHandling: 'merge'
        });
      },
    });
  }

  authorsQuery: Observable<ApolloQueryResult<GetAuthorsQuery>> =
    this._authorsQueryRef.valueChanges;

  changePage(pageNumber: number) {
    this._queryVariables.skip = (pageNumber - 1) * this._queryVariables.take;
    this._authorsQueryRef.refetch(this._queryVariables);
  }

  changePageSize(selectedPageSize: number) {
    this._queryVariables.take = selectedPageSize;
    this._authorsQueryRef.refetch(this._queryVariables);
  }
}
