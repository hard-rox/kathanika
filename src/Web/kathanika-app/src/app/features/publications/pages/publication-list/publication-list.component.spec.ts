import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationListComponent } from './publication-list.component';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { of } from 'rxjs';
import { GetPublicationsGQL } from 'src/app/graphql/generated/graphql-operations';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';
import { RouterTestingModule } from '@angular/router/testing';
import { PaginationModule } from 'src/app/shared/modules/pagination/pagination.module';

describe('PublicationListComponent', () => {
  let component: PublicationListComponent;
  let fixture: ComponentFixture<PublicationListComponent>;
  let router: Router;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      declarations: [PublicationListComponent],
      imports: [PaginationModule, RouterTestingModule],
      providers: [
        {
          provide: GetPublicationsGQL,
          useValue: mockQueryGql,
        },
        {
          provide: ActivatedRoute,
          useValue: {
            queryParams: of({
              size: 20,
              page: 2,
            }),
          },
        },
      ],
    })
      .compileComponents()
      .then(() => {
        fixture = TestBed.createComponent(PublicationListComponent);
        component = fixture.componentInstance;
        router = TestBed.inject(Router);
      });
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
    expect(queryVariables.skip).toEqual(20);
    expect(queryVariables.take).toEqual(20);
  });

  it('should refetch query on ngOnInit', () => {
    const queryVariables = component.queryVariables;
    const queryRefRefetchSpy = spyOn<any>(component.queryRef, 'refetch');
    component.ngOnInit();
    expect(queryRefRefetchSpy).toHaveBeenCalledOnceWith(queryVariables);
  });
});
