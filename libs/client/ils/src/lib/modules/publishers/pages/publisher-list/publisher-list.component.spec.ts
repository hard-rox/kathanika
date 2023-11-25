import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherListComponent } from './publisher-list.component';
import { GetPublishersGQL } from '@kathanika/graphql-consumer';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';
import { of } from 'rxjs';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute } from '@angular/router';

describe('PublisherListComponent', () => {
  let component: PublisherListComponent;
  let fixture: ComponentFixture<PublisherListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublisherListComponent],
      imports: [PaginationComponent, RouterTestingModule],
      providers: [
        {
          provide: GetPublishersGQL,
          useValue: mockQueryGql
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
      ]
    });
    fixture = TestBed.createComponent(PublisherListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
