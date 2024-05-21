import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorListComponent } from './author-list.component';
import { AuthorFilterInput, DeleteAuthorGQL, GetAuthorsGQL, mockMutationGql, mockQueryGql } from '@kathanika/graphql-ts-client';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { of } from 'rxjs';
import { KnPagination } from '@kathanika/kn-ui';

describe('AuthorListComponent', () => {
  let component: AuthorListComponent;
  let fixture: ComponentFixture<AuthorListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AuthorListComponent],
      imports: [KnPagination, RouterModule.forRoot([])],
      providers: [
        {
          provide: GetAuthorsGQL,
          useValue: mockQueryGql,
        },
        {
          provide: DeleteAuthorGQL,
          useValue: mockMutationGql,
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
    const filter: AuthorFilterInput = {
      or: [
        {
          firstName: {
            contains: 'John',
          },
        },
        {
          lastName: {
            contains: 'John'
          },
        },
        {
          nationality: {
            contains: 'John'
          },
        },
      ],
    }
    component['setSearchTextQueryFilter'](searchText);

    expect(component.queryVariables.filter).toEqual(filter);
  });
});
