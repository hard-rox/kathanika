import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorDetailsComponent } from './author-details.component';
import { GetAuthorGQL, mockQueryGql } from '@kathanika/graphql-ts-client';
import { ActivatedRoute, RouterModule } from '@angular/router';

describe('AuthorDetailsComponent', () => {
  let component: AuthorDetailsComponent;
  let fixture: ComponentFixture<AuthorDetailsComponent>;
  let activatedRoute: ActivatedRoute;

  const mockAuthorId = '12345';

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterModule.forRoot([])
      ],
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
    const queryRefRefetchSpy = jest.spyOn(component.queryRef, 'refetch');
    component.ngOnInit();
    const queryVariable = component.queryVariables;
    expect(queryVariable?.id).toEqual(mockAuthorId);
    expect(queryRefRefetchSpy).toHaveBeenCalledWith(queryVariable);
  });

  it('should not refetch query and variable with route param when route param id is invalid on ngOnInit', () => {
    const queryRefRefetchSpy = jest.spyOn(component.queryRef, 'refetch');
    activatedRoute.snapshot.params['id'] = '';

    component.ngOnInit();

    const queryVariable = component.queryVariables;
    expect(queryVariable).toBeFalsy();
    expect(queryRefRefetchSpy).not.toHaveBeenCalled();
  });
});
