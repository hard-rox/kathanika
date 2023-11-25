import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorDetailsComponent } from './author-details.component';
import { GetAuthorGQL } from '@kathanika/graphql-consumer';
import { ActivatedRoute } from '@angular/router';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';
import { RouterTestingModule } from '@angular/router/testing';

describe('AuthorDetailsComponent', () => {
  let component: AuthorDetailsComponent;
  let fixture: ComponentFixture<AuthorDetailsComponent>;
  let activatedRoute: ActivatedRoute;

  const mockAuthorId = '12345';

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [AuthorDetailsComponent],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: {
                id: mockAuthorId,
              },
            },
          },
        },
        {
          provide: GetAuthorGQL,
          useValue: mockQueryGql,
        },
      ],
    }).compileComponents();

    activatedRoute = TestBed.inject(ActivatedRoute);
    fixture = TestBed.createComponent(AuthorDetailsComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should refetch query with route param on ngOnInit', () => {
    const queryRefRefetchSpy = spyOn(component.queryRef, 'refetch');
    component.ngOnInit();
    const queryVariable = component.queryVariables;
    expect(queryVariable?.id).toEqual(mockAuthorId);
    expect(queryRefRefetchSpy).toHaveBeenCalledOnceWith(queryVariable);
  });

  it('should not refetch query and variable with route param when route param id is invalid on ngOnInit', () => {
    const queryRefRefetchSpy = spyOn(component.queryRef, 'refetch');
    activatedRoute.snapshot.params['id'] = '';

    component.ngOnInit();

    const queryVariable = component.queryVariables;
    expect(queryVariable).toBeFalsy();
    expect(queryRefRefetchSpy).not.toHaveBeenCalled();
  });
});
