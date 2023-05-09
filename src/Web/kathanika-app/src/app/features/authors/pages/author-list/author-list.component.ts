import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
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

  private setQueryVariableFromRouteQueryParams(params: Params) {
    let size = +params['size'];
    let page = +params['page'];
    let sortBy = params['sortBy'] as string;
    let order = params['order'] as string;

    if (size && size > 0) this.pageSize = this._queryVariables.take = size;
    if (page && page > 0) this._queryVariables.skip = (page - 1) * size;
    // if (sortBy && sortBy.length > 0) this._queryVariables.filter = {};
  }

  private setQueryParams() {
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: {
        page:
          this._queryVariables.skip == 0
            ? 1
            : this._queryVariables.skip / this._queryVariables.take + 1,
        size: this._queryVariables.take,
        sortBy: 'lastName',
        order: SortEnumType.Asc,
      },
      queryParamsHandling: 'merge',
    });
  }

  constructor(
    private getAuthorsGql: GetAuthorsGQL,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  pageSize: number = 0;

  authorsQuery: Observable<ApolloQueryResult<GetAuthorsQuery>> =
    this._authorsQueryRef.valueChanges;

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe({
      next: (params) => {
        this.setQueryVariableFromRouteQueryParams(params);
        this.setQueryParams();
        this._authorsQueryRef.refetch(this._queryVariables);
      },
    });
  }

  changePage(pageNumber: number) {
    this._queryVariables.skip = (pageNumber - 1) * this._queryVariables.take;
    this._authorsQueryRef.refetch(this._queryVariables);
    this.setQueryParams();
  }

  changePageSize(selectedPageSize: number) {
    this._queryVariables.take = selectedPageSize;
    this._queryVariables.skip = 0;
    this._authorsQueryRef.refetch(this._queryVariables);
    this.setQueryParams();
  }
}
