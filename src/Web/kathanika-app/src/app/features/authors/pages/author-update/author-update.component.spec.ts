import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorUpdateComponent } from './author-update.component';
import { ActivatedRoute } from '@angular/router';
import {
  GetAuthorGQL,
  GetAuthorQuery,
  UpdateAuthorGQL,
  UpdateAuthorMutation,
} from 'src/app/graphql/generated/graphql-operations';
import { mockMutationGql, mockQueryGql } from 'src/test-utils/gql-test-utils';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { of } from 'rxjs';
import { MutationResult } from 'apollo-angular/types';
import { ApolloQueryResult } from '@apollo/client/core/types';
import { PanelComponent } from 'src/app/shared/components/panel/panel.component';
import { DateInputComponent } from 'src/app/shared/components/date-input/date-input.component';
import { TextInputComponent } from 'src/app/shared/components/text-input/text-input.component';
import { TextareaInputComponent } from 'src/app/shared/components/textarea-input/textarea-input.component';
import { ToggleComponent } from 'src/app/shared/components/toggle/toggle.component';

describe('AuthorUpdateComponent', () => {
  const routeParam = '12345';
  let component: AuthorUpdateComponent;
  let fixture: ComponentFixture<AuthorUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        PanelComponent,
        TextInputComponent,
        DateInputComponent,
        TextareaInputComponent,
        ToggleComponent
      ],
      declarations: [
        AuthorFormComponent,
        AuthorUpdateComponent,
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
          provide: UpdateAuthorGQL,
          useValue: mockMutationGql,
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
      subscribe: () => { },
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
    const mutationResult: MutationResult<UpdateAuthorMutation> = {
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
      markedAsDeceased: false,
      dateOfDeath: null,
      nationality: '',
      biography: '',
    });
    expect(alertServiceSpy).toHaveBeenCalled();
  });

  it('should populate errors array on onValidFormSubmit when errors', () => {
    const mutationResult: MutationResult<UpdateAuthorMutation> = {
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
      markedAsDeceased: false,
      dateOfDeath: null,
      nationality: '',
      biography: '',
    });
    expect(component.errors.length).toEqual(3);
  });

  it('should clear errors array on close button click in alert component', () => {
    component.errors = ['Test error'];
    component.closeAlert();
    expect(component.errors.length).toEqual(0);
  });
});
