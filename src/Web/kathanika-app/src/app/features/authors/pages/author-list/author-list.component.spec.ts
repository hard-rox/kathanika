import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorListComponent } from './author-list.component';
import { GetAuthorsGQL } from 'src/app/graphql/generated/graphql-operations';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';
import { PaginationModule } from 'src/app/shared/modules/pagination/pagination.module';

describe('AuthorListComponent', () => {
  let component: AuthorListComponent;
  let fixture: ComponentFixture<AuthorListComponent>;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AuthorListComponent],
      imports: [PaginationModule, RouterTestingModule],
      providers: [
        {
          provide: GetAuthorsGQL,
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
    fixture = TestBed.createComponent(AuthorListComponent);
    component = fixture.componentInstance;
    router = TestBed.inject(Router);
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('hould change _queryVariables on changePage', () => {
    let queryVariables = component.queryVariables;
    const page = 2;

    component.changePage(page);
    expect(queryVariables.skip).toEqual((page - 1) * queryVariables.take);
  });

  it('should refetch query changePage', () => {
    const queryRefRefetchSpy = spyOn<any>(component.queryRef, 'refetch');
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
    let queryVariables = component.queryVariables;
    const pageSize = 2;

    component.changePageSize(pageSize);
    expect(queryVariables.take).toEqual(pageSize);
  });

  it('should refetch query on changePageSize', () => {
    const queryRefRefetchSpy = spyOn<any>(component.queryRef, 'refetch');
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

  it('should change _QueryVariables on ngOnInit', () => {
    // size: 3, page: 2
    const queryVariables = component.queryVariables;
    component.ngOnInit();
    expect(queryVariables.skip).toEqual(3);
    expect(queryVariables.take).toEqual(3);
  });

  it('should refetch query on ngOnInit', () => {
    const queryVariables = component.queryVariables;
    const queryRefRefetchSpy = spyOn<any>(component.queryRef, 'refetch');
    component.ngOnInit();
    expect(queryRefRefetchSpy).toHaveBeenCalledOnceWith(queryVariables);
  });
});
