import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import {
  GetAuthorsGQL,
  GetAuthorsQuery,
  GetAuthorsQueryVariables,
  SortEnumType,
} from 'src/app/graphql/generated/graphql-operations';
import { BaseQueryComponent } from 'src/app/shared/bases/base-query-component';
import { MessageAlertService } from 'src/app/core/services/message-alert.service';

@Component({
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.scss'],
})
export class AuthorListComponent
  extends BaseQueryComponent<GetAuthorsQuery, GetAuthorsQueryVariables>
  implements OnInit
{
  private setQueryVariableFromRouteQueryParams(params: Params) {
    let size = +params['size'];
    let page = +params['page'];
    let sortBy = params['sortBy'] as string;
    let order = params['order'] as string;

    if (size && size > 0) this.pageSize = this.queryVariables.take = size;
    if (page && page > 0) this.queryVariables.skip = (page - 1) * size;
    // if (sortBy && sortBy.length > 0) this._queryVariables.filter = {};
  }

  private setQueryParams() {
    this.router.navigate([], {
      relativeTo: this.activatedRoute,
      queryParams: {
        page:
          this.queryVariables.skip == 0
            ? 1
            : this.queryVariables.skip / this.queryVariables.take + 1,
        size: this.queryVariables.take,
        sortBy: 'firstName',
        order: SortEnumType.Asc,
      },
      queryParamsHandling: 'merge',
    });
  }

  constructor(
    private gql: GetAuthorsGQL,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private alertService: MessageAlertService
  ) {
    super(gql, {
      skip: 0,
      take: 10,
      sortBy: {
        firstName: SortEnumType.Asc,
      },
    });
  }

  pageSizes: number[] = [10, 20, 50, 100];
  pageSize: number = 0;

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe({
      next: (params) => {
        this.setQueryVariableFromRouteQueryParams(params);
        this.setQueryParams();
        this.queryRef.refetch(this.queryVariables);
      },
    });
  }

  changePage(pageNumber: number) {
    this.queryVariables.skip = (pageNumber - 1) * this.queryVariables.take;
    this.queryRef.refetch(this.queryVariables);
    this.setQueryParams();
  }

  changePageSize(selectedPageSize: number) {
    this.queryVariables.take = selectedPageSize;
    this.queryVariables.skip = 0;
    this.queryRef.refetch(this.queryVariables);
    this.setQueryParams();
  }

  deleteAuthor(id: string, name: string) {
    this.alertService
      .showConfirmation(
        'warning',
        `Are you sure deleting author ${name}? This action is irreversible.`,
        'Confirm deleting author?',
        'Yes, Delete'
      )
      .subscribe({
        next: (isConfirmed) => {
          if (isConfirmed) {
            //TODO deleting author functionality...
          }
        },
      });
  }
}
