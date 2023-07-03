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
import { Subject, debounceTime, distinctUntilChanged, map, switchMap } from 'rxjs';

@Component({
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.scss'],
})
export class AuthorListComponent
  extends BaseQueryComponent<GetAuthorsQuery, GetAuthorsQueryVariables>
  implements OnInit
{
  private searchTextSubject = new Subject<string>();

  private setQueryFilter(searchText: string) {
    if (!searchText || searchText.length == 0) {
      this.queryVariables.filter = null;
      return;
    }

    this.queryVariables.filter = {
      or: [
        {
          firstName: {
            contains: searchText,
          },
        },
        {
          lastName: {
            contains: searchText,
          },
        },
        {
          nationality: {
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
    gql: GetAuthorsGQL,
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

  searchText: string | null = null;
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

    this.searchTextSubject.pipe(
      debounceTime(700),
      distinctUntilChanged(),
      map((value) => {
        // console.debug(value);
        this.searchText = value;
        this.setQueryFilter(value);
        this.queryRef.refetch(this.queryVariables);
        this.setQueryParams();
      })
    ).subscribe();
  }

  onSearchTextChanged($event: any) {
    const searchText = $event.target.value;
    this.searchTextSubject.next(searchText);
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
