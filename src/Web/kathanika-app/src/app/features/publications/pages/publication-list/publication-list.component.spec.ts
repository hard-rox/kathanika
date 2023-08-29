import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationListComponent } from './publication-list.component';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { of } from 'rxjs';
import { GetPublicationsGQL } from 'src/app/graphql/generated/graphql-operations';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';
import { RouterTestingModule } from '@angular/router/testing';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';

describe('PublicationListComponent', () => {
  let component: PublicationListComponent;
  let fixture: ComponentFixture<PublicationListComponent>;
  let router: Router;

  beforeEach(async () => {
    TestBed.configureTestingModule({
      declarations: [PublicationListComponent],
      imports: [PaginationComponent, RouterTestingModule],
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

  it('should call base "init()" on ngOnInit', () => {
    const spy = spyOn<any>(component, 'init');
    component.ngOnInit();
    expect(spy).toHaveBeenCalledTimes(1);
  });
});
