import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorAddComponent } from './author-add.component';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import {
  AddAuthorGQL,
  AddAuthorMutation,
} from 'src/app/graphql/generated/graphql-operations';
import { mockMutationGql } from 'src/test-utils/gql-test-utils';
import { of } from 'rxjs';
import { MutationResult } from 'apollo-angular';
import { PanelComponent } from 'src/app/shared/components/panel/panel.component';
import { DateInputComponent } from 'src/app/shared/components/date-input/date-input.component';
import { TextInputComponent } from 'src/app/shared/components/text-input/text-input.component';
import { TextareaInputComponent } from 'src/app/shared/components/textarea-input/textarea-input.component';
import { AuthorFormOutput } from '../../types/author-form-output';
import { ToggleComponent } from 'src/app/shared/components/toggle/toggle.component';

describe('AuthorAddComponent', () => {
  let component: AuthorAddComponent;
  let fixture: ComponentFixture<AuthorAddComponent>;

  const formOutput: AuthorFormOutput = {
    firstName: 'Hello',
    lastName: 'world',
    dateOfBirth: new Date('2023-01-01'),
    markedAsDeceased: false,
    dateOfDeath: null,
    nationality: 'USA',
    biography: 'Test',
  };

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
      declarations: [AuthorAddComponent, AuthorFormComponent],
      providers: [
        {
          provide: AddAuthorGQL,
          useValue: mockMutationGql,
        },
      ],
    });
    fixture = TestBed.createComponent(AuthorAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should turn off loading panel after onValidFormSubmit', () => {
    const mockMutationResult: MutationResult<AddAuthorMutation> = {
      loading: false,
      data: {
        addAuthor: {
          message: null,
          data: { id: '12345' },
          errors: null,
          __typename: 'AddAuthorPayload',
        },
      }
    };
    spyOn(component['router'], 'navigate');
    spyOn(component['gql'], 'mutate').and.returnValue(of(mockMutationResult));

    expect(component.isPanelLoading).not.toBeTrue();
    component.onValidFormSubmit(formOutput);
    expect(component.isPanelLoading).not.toBeTrue();
  });

  it('should fill error array on error in mutation subscribe', () => {
    const mockMutationResult: MutationResult<AddAuthorMutation> = {
      loading: false,
      data: {
        addAuthor: {
          message: null,
          data: null,
          errors: [
            {
              fieldName: 'DateOfBirth',
              __typename: 'InvalidFieldError',
              message: "Cann't be future date",
            },
          ],
          __typename: 'AddAuthorPayload',
        },
      },
    };
    spyOn(component['gql'], 'mutate').and.returnValue(of(mockMutationResult));
    component.onValidFormSubmit(formOutput);
    expect(component.errors.length).toEqual(1);
  });

  it('should call showToast onValidFormSubmit', () => {
    const mockMutationResult: MutationResult<AddAuthorMutation> = {
      loading: false,
      data: {
        addAuthor: {
          message: null,
          data: { id: '12345' },
          errors: null,
          __typename: 'AddAuthorPayload',
        },
      }
    };
    spyOn(component['router'], 'navigate');
    spyOn(component['gql'], 'mutate').and.returnValue(of(mockMutationResult));
    const alertServiceSpy = spyOn(component['alertService'], 'showToast');

    component.onValidFormSubmit(formOutput);
    expect(alertServiceSpy).toHaveBeenCalledTimes(1);
  });

  it('should clear errors array on close button click in alert component', () => {
    component.errors = ['Test error'];
    component.closeAlert();
    expect(component.errors.length).toEqual(0);
  });
});
