import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorListComponent } from './author-list.component';
import { DeleteAuthorGQL, GetAuthorsGQL } from '@kathanika/graphql-ts-client';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { KnPagination } from '@kathanika/kn-ui';
import {
  mockQueryGql,
  mockMutationGql,
} from '../../../../test-utils/gql-test-utils';

describe('AuthorListComponent', () => {
  let component: AuthorListComponent;
  let fixture: ComponentFixture<AuthorListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AuthorListComponent],
      imports: [KnPagination, RouterTestingModule],
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

  // it('should call base "init()" on ngOnInit', () => {
  //   const spy = spyOn(component, 'init');
  //   component.ngOnInit();
  //   expect(spy).toHaveBeenCalledTimes(1);
  // });
});
