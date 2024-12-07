import { BaseQueryComponent } from './base-query-component';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { OperationVariables } from '@apollo/client/core';
import { Query } from 'apollo-angular';
import { Subject, debounceTime, distinctUntilChanged, map } from 'rxjs';

export abstract class BasePaginatedListComponent<
    TQuery,
    TQueryVariables extends PaginationQueryVariables,
> extends BaseQueryComponent<TQuery, TQueryVariables> {
    protected abstract setSearchTextQueryFilter(searchText: string): void;

    private setQueryVariableFromRouteQueryParams(params: Params) {
        const size = +params['size'];
        const page = +params['page'];
        const searchText = params['searchText'] as string | null;

        if (size && size > 0) this.pageSize = this.queryVariables.take = size;
        if (page && page > 0) this.queryVariables.skip = (page - 1) * size;
        if (searchText && searchText.length > 0) {
            this.setSearchTextQueryFilter(searchText);
            this.searchText = searchText;
        }
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

    protected init() {
        this.activatedRoute.queryParams.subscribe({
            next: (params) => {
                this.setQueryVariableFromRouteQueryParams(params);
                this.setQueryParams();
                this.queryRef.refetch(this.queryVariables);
            },
        });

        this.searchTextSubject
            .pipe(
                debounceTime(700),
                distinctUntilChanged(),
                map((value) => {
                    this.searchText = value;
                    this.setSearchTextQueryFilter(value);
                    this.queryRef.refetch(this.queryVariables);
                    this.setQueryParams();
                }),
            )
            .subscribe();
    }

    protected searchTextSubject = new Subject<string>();

    protected constructor(
        gql: Query<TQuery, TQueryVariables>,
        private readonly activatedRoute: ActivatedRoute,
        private readonly router: Router,
        paginationQueryVariables: TQueryVariables,
    ) {
        super(gql, paginationQueryVariables);
    }

    searchText: string | null = null;
    pageSize!: number;

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    protected onSearchTextChanged($event: any) {
        const searchText = $event.target.value;
        this.searchTextSubject.next(searchText);
    }

    protected changePage(pageNumber: number) {
        this.queryVariables.skip = (pageNumber - 1) * this.queryVariables.take;
        this.queryRef.refetch(this.queryVariables);
        this.setQueryParams();
    }

    protected changePageSize(selectedPageSize: number) {
        this.queryVariables.take = selectedPageSize;
        this.queryVariables.skip = 0;
        this.queryRef.refetch(this.queryVariables);
        this.setQueryParams();
    }
}

export interface PaginationQueryVariables extends OperationVariables {
    skip: number;
    take: number;
}