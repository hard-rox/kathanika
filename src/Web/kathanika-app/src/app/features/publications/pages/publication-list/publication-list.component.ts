import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { MessageAlertService } from 'src/app/core/services/message-alert.service';
import {
  GetPublicationsGQL,
  GetPublicationsQuery,
  GetPublicationsQueryVariables,
  SortEnumType,
} from 'src/app/graphql/generated/graphql-operations';
import { BaseQueryComponent } from 'src/app/shared/bases/base-query-component';

@Component({
  templateUrl: './publication-list.component.html',
  styleUrls: ['./publication-list.component.scss'],
})
export class PublicationListComponent
  extends BaseQueryComponent<
    GetPublicationsQuery,
    GetPublicationsQueryVariables
  >
  implements OnInit
{
  private setQueryFilter(searchText: string) {
    if (!searchText || searchText.length == 0) {
      this.queryVariables.filter = null;
      return;
    }

    this.queryVariables.filter = {
      or: [
        {
          title: {
            contains: searchText,
          },
        },
        {
          isbn: {
            contains: searchText,
          },
        },
        {
          publisher: {
            contains: searchText,
          },
        },
        {
          language: {
            contains: searchText,
          },
        },
      ],
    };
  }

  private setQueryVariableFromRouteQueryParams(params: Params) {
    const size = +params['size'];
    const page = +params['page'];
    const searchText = params['searchText'] as string | null;

    if (size && size > 0) this.pageSize = this.queryVariables.take = size;
    if (page && page > 0) this.queryVariables.skip = (page - 1) * size;
    if (searchText && searchText.length > 0) this.setQueryFilter(searchText);
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
        searchText: this.searchText,
      },
      queryParamsHandling: 'merge',
    });
  }

  constructor(
    gql: GetPublicationsGQL,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private alertService: MessageAlertService
  ) {
    super(gql, {
      skip: 0,
      take: 20,
      sortBy: {
        title: SortEnumType.Asc,
      },
    });
  }

  searchText: string | null = null;
  pageSizes: number[] = [10, 20, 50, 100];
  pageSize: number = this.pageSizes[0];

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
}
