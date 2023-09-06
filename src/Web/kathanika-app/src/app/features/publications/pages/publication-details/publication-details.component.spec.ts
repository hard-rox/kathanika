import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetPublicationGQL } from 'src/app/graphql/generated/graphql-operations';
import { ActivatedRoute } from '@angular/router';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';
import { RouterTestingModule } from '@angular/router/testing';
import { PublicationDetailsComponent } from './publication-details.component';

describe('PublicationDetailsComponent', () => {
  let component: PublicationDetailsComponent;
  let fixture: ComponentFixture<PublicationDetailsComponent>;
  let activatedRoute: ActivatedRoute;

  const mockPublicationId = '12345';

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [PublicationDetailsComponent],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: {
                id: mockPublicationId,
              },
            },
          },
        },
        {
          provide: GetPublicationGQL,
          useValue: mockQueryGql,
        },
      ],
    }).compileComponents();

    activatedRoute = TestBed.inject(ActivatedRoute);
    fixture = TestBed.createComponent(PublicationDetailsComponent);
    component = fixture.componentInstance;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should refetch query with route param on ngOnInit', () => {
    const queryRefRefetchSpy = spyOn<any>(component.queryRef, 'refetch');
    component.ngOnInit();
    const queryVariable = component.queryVariables;
    expect(queryVariable?.id).toEqual(mockPublicationId);
    expect(queryRefRefetchSpy).toHaveBeenCalledOnceWith(queryVariable);
  });

  it('should not refetch query and variable with route param when route param id is invalid on ngOnInit', () => {
    const queryRefRefetchSpy = spyOn<any>(component.queryRef, 'refetch');
    activatedRoute.snapshot.params['id'] = '';

    component.ngOnInit();

    const queryVariable = component.queryVariables;
    expect(queryVariable).toBeFalsy();
    expect(queryRefRefetchSpy).not.toHaveBeenCalled();
  });
});
