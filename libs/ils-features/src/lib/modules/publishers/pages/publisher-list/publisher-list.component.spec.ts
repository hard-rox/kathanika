import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherListComponent } from './publisher-list.component';
import { GetPublishersGQL, PublisherFilterInput, mockQueryGql } from '@kathanika/graphql-ts-client';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { KnPagination } from '@kathanika/kn-ui';
import { of } from 'rxjs';

describe('PublisherListComponent', () => {
  let component: PublisherListComponent;
  let fixture: ComponentFixture<PublisherListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublisherListComponent],
      imports: [
        RouterModule.forRoot([]),
        KnPagination
      ],
      providers: [
        {
          provide: GetPublishersGQL,
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
    });
    fixture = TestBed.createComponent(PublisherListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set queryVariables.filter correctly when searchText is empty', () => {
    component['setSearchTextQueryFilter']('');
    expect(component.queryVariables.filter).toBeNull();
  });

  it('should set queryVariables.filter correctly when searchText is not empty', () => {
    const searchText = 'John';
    const filter: PublisherFilterInput = {
      or: [
        {
          name: {
            contains: 'John',
          },
        },
        {
          description: {
            contains: 'John'
          },
        },
      ],
    }
    component['setSearchTextQueryFilter'](searchText);

    expect(component.queryVariables.filter).toEqual(filter);
  });
});
