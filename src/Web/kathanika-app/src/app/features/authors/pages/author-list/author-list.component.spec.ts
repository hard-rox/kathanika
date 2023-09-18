import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorListComponent } from './author-list.component';
import { GetAuthorsGQL } from 'src/app/graphql/generated/graphql-operations';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';

describe('AuthorListComponent', () => {
  let component: AuthorListComponent;
  let fixture: ComponentFixture<AuthorListComponent>;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AuthorListComponent],
      imports: [PaginationComponent, RouterTestingModule],
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

  it('should call base "init()" on ngOnInit', () => {
    const spy = spyOn<any>(component, 'init');
    component.ngOnInit();
    expect(spy).toHaveBeenCalledTimes(1);
  });
});
