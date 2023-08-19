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
});
