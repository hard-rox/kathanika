import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorAddComponent } from './author-add.component';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddAuthorGQL, AddAuthorMutation } from '@kathanika/graphql-ts-client';
import {
  KnPanel,
  KnTextInput,
  KnDateInput,
  KnTextareaInput,
  KnToggle,
} from '@kathanika/kn-ui';
import { MutationResult } from 'apollo-angular';
import { of } from 'rxjs';
import { mockMutationGql } from '../../../../test-utils/gql-test-utils';
import { AuthorFormOutput } from '../../types/author-form-output';

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
        KnPanel,
        KnTextInput,
        KnDateInput,
        KnTextareaInput,
        KnToggle
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
      },
    };
    jest.spyOn(component['router'], 'navigate');
    jest
      .spyOn(component['gql'], 'mutate')
      .mockReturnValue(of(mockMutationResult));

    expect(component.isPanelLoading).not.toBeTruthy();
    component.onValidFormSubmit(formOutput);
    expect(component.isPanelLoading).not.toBeTruthy();
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
    jest
      .spyOn(component['gql'], 'mutate')
      .mockReturnValue(of(mockMutationResult));
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
      },
    };
    jest.spyOn(component['router'], 'navigate');
    jest
      .spyOn(component['gql'], 'mutate')
      .mockReturnValue(of(mockMutationResult));
    const alertServiceSpy = jest.spyOn(component['alertService'], 'showToast');

    component.onValidFormSubmit(formOutput);
    expect(alertServiceSpy).toHaveBeenCalledTimes(1);
  });

  it('should clear errors array on close button click in alert component', () => {
    component.errors = ['Test error'];
    component.closeAlert();
    expect(component.errors.length).toEqual(0);
  });
});
