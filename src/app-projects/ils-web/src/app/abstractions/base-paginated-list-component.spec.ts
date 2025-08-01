import {ComponentFixture, TestBed} from '@angular/core/testing';
import {
    BasePaginatedListComponent,
    PaginationQueryVariables,
} from './base-paginated-list-component';
import {ActivatedRoute, Params, Router} from '@angular/router';
import {RouterTestingModule} from '@angular/router/testing';
import {Component, inject} from '@angular/core';
import {of} from 'rxjs';
import {Query} from 'apollo-angular';
import {mockQueryGql} from "../graphql/gql-test-utils";

@Component({
    template: ''
})
class BasePaginatedListTestingComponent extends BasePaginatedListComponent<unknown, PaginationQueryVariables> {
    protected setSearchTextQueryFilter(searchText: string): void {
        throw new Error(`Method not implemented. ${searchText}`);
    }

    constructor() {
        const gql = inject(Query<unknown, PaginationQueryVariables>);
        const activatedRoute = inject(ActivatedRoute);
        const router = inject(Router);
        super(gql, activatedRoute, router, {
            skip: 0,
            take: 10,
        });
    }

    override changePage(pageNumber: number): void {
        super.changePage(pageNumber);
    }

    override changePageSize(selectedPageSize: number): void {
        super.changePageSize(selectedPageSize);
    }
}

describe('BasePaginatedListComponent', () => {
    let component: BasePaginatedListTestingComponent;
    let fixture: ComponentFixture<BasePaginatedListTestingComponent>;
    let router: Router;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [RouterTestingModule, BasePaginatedListTestingComponent],
            providers: [
                {
                    provide: Query<never, PaginationQueryVariables>,
                    useValue: mockQueryGql,
                },
                {
                    provide: ActivatedRoute,
                    useValue: {
                        queryParams: of({
                            size: 3,
                            page: 2,
                        }),
                    },
                },
            ],
        }).compileComponents();

        fixture = TestBed.createComponent(BasePaginatedListTestingComponent);
        component = fixture.componentInstance;
        router = TestBed.inject(Router);
    });

    it('should change _queryVariables on changePage', () => {
        const queryVariables = component.queryVariables;
        const page = 2;

        component.changePage(page);
        expect(queryVariables.skip).toEqual((page - 1) * queryVariables.take);
    });

    it('should refetch query changePage', () => {
        const queryRefRefetchSpy = jest.spyOn(component.queryRef, 'refetch');
        const page = 2;

        component.changePage(page);
        expect(queryRefRefetchSpy).toHaveBeenCalled();
    });

    it('should change route query params on changePage', () => {
        const page = 2;
        component.changePage(page);
        const routeQueryParams = router.getCurrentNavigation()?.extras
            .queryParams as Params;
        expect(routeQueryParams).toBeTruthy();
        expect(routeQueryParams['page']).toEqual(page);
    });

    it('should change _queryVariables on changePageSize', () => {
        const queryVariables = component.queryVariables;
        const pageSize = 2;

        component.changePageSize(pageSize);
        expect(queryVariables.take).toEqual(pageSize);
    });

    it('should refetch query on changePageSize', () => {
        const queryRefRefetchSpy = jest.spyOn(component.queryRef, 'refetch');
        const pageSize = 2;

        component.changePageSize(pageSize);
        expect(queryRefRefetchSpy).toHaveBeenCalled();
    });

    it('should change route query params on changePageSize', () => {
        const pageSize = 2;
        component.changePageSize(pageSize);
        const routeQueryParams = router.getCurrentNavigation()?.extras
            .queryParams as Params;
        expect(routeQueryParams).toBeTruthy();
        expect(routeQueryParams['size']).toEqual(pageSize);
    });

    it('should change _QueryVariables on init', () => {
        // size: 3, page: 2
        const queryVariables = component.queryVariables;
        component['init']();
        expect(queryVariables.skip).toEqual(3);
        expect(queryVariables.take).toEqual(3);
    });

    it('should refetch query on init', () => {
        const queryVariables = component.queryVariables;
        const queryRefRefetchSpy = jest.spyOn(component.queryRef, 'refetch');
        component['init']();
        expect(queryRefRefetchSpy).toHaveBeenCalledWith(queryVariables);
    });
});