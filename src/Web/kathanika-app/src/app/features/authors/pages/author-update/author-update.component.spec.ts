import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorUpdateComponent } from './author-update.component';
import { ActivatedRoute } from '@angular/router';
import {
  GetAuthorGQL,
  GetAuthorQuery,
  UpadateAuthorGQL,
  UpadateAuthorMutation,
} from 'src/app/graphql/generated/graphql-operations';
import { mockMutatuionGql, mockQueryGql } from 'src/test-utils/gql-test-utils';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PanelComponent } from 'src/app/shared/modules/panel/components/panel/panel.component';
import { of } from 'rxjs';
import { MutationResult } from 'apollo-angular/types';
import { ApolloQueryResult } from '@apollo/client/core/types';

describe('AuthorUpdateComponent', () => {
  const routeParam = '12345';
  let component: AuthorUpdateComponent;
  let fixture: ComponentFixture<AuthorUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      declarations: [
        AuthorFormComponent,
        AuthorUpdateComponent,
        PanelComponent,
      ],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: {
                id: routeParam,
              },
            },
          },
        },
        {
          provide: GetAuthorGQL,
          useValue: mockQueryGql,
        },
        {
          provide: UpadateAuthorGQL,
          useValue: mockMutatuionGql,
        },
      ],
    });
    fixture = TestBed.createComponent(AuthorUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch with valid route param id in ngOnInit', () => {
    const gqlSpy = spyOn<any>(component['gql'], 'fetch').and.returnValue({
      subscribe: () => {},
    });
    component.ngOnInit();
    expect(gqlSpy).toHaveBeenCalledOnceWith({ id: routeParam });
  });

  it('should show Error pop up on null author when getting author', () => {
    const mockQueryResult: ApolloQueryResult<GetAuthorQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        author: null
      }
    }
    spyOn(component['gql'], 'fetch').and.returnValue(of(mockQueryResult));
    spyOn(component['router'], 'navigate');
    const alertServiceSpy = spyOn(component['alertService'], 'showPopup');
    component.ngOnInit();
    expect(alertServiceSpy).toHaveBeenCalledOnceWith('error', 'Returning to list page.', 'Author not found');
  });

  it('should call "showToast" on onValidFormSubmit when no errors', () => {
    const alertServiceSpy = spyOn(component['alertService'], 'showToast');
    const mutationResult: MutationResult<UpadateAuthorMutation> = {
      data: {
        updateAuthor: {
          message: 'Author Hello World updated successfully.',
          data: { id: '6476322e481b1b2c3173cebe', __typename: 'Author' },
          errors: null,
          __typename: 'UpdateAuthorPayload',
        },
      },
      loading: false,
    };
    spyOn(component['mutation'], 'mutate').and.returnValue(of(mutationResult));
    spyOn(component['router'], 'navigate');
    component.onValidFormSubmit({
      firstName: '',
      lastName: '',
      dateOfBirth: '',
      dateOfDeath: null,
      nationality: '',
      biography: '',
    });
    expect(alertServiceSpy).toHaveBeenCalled();
  });

  it('should populate errors array on onValidFormSubmit when errors', () => {
    const mutationResult: MutationResult<UpadateAuthorMutation> = {
      data: {
        updateAuthor: {
          data: null,
          errors: [
            {
              fieldName: 'Test',
              message: 'Message',
              __typename: 'InvalidFieldError'
            },
            {
              objectName: 'Test',
              id: '12345',
              message: 'Message',
              __typename: 'NotFoundWithTheIdError'
            },
            {
              fieldName: 'Test',
              message: 'Message'
            }
          ],
          __typename: 'UpdateAuthorPayload',
        },
      },
      loading: false,
    };
    spyOn(component['mutation'], 'mutate').and.returnValue(of(mutationResult));
    component.onValidFormSubmit({
      firstName: '',
      lastName: '',
      dateOfBirth: '',
      dateOfDeath: null,
      nationality: '',
      biography: '',
    });
    expect(component.errors.length).toEqual(3);
  });
});
