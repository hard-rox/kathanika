import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorAddComponent } from './author-add.component';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import {
  AddAuthorGQL,
  AddAuthorMutation,
} from 'src/app/graphql/generated/graphql-operations';
import { mockMutatuionGql } from 'src/test-utils/gql-test-utils';
import { PanelComponent } from 'src/app/shared/modules/panel/components/panel/panel.component';
import { of } from 'rxjs';
import { MutationResult } from 'apollo-angular';

describe('AuthorAddComponent', () => {
  let component: AuthorAddComponent;
  let fixture: ComponentFixture<AuthorAddComponent>;

  const formOutput = {
    firstName: 'Hello',
    lastName: 'world',
    dateOfBirth: '2023-01-01',
    dateOfDeath: null,
    nationality: 'USA',
    biography: 'Test',
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      declarations: [AuthorAddComponent, AuthorFormComponent, PanelComponent],
      providers: [
        {
          provide: AddAuthorGQL,
          useValue: mockMutatuionGql,
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

  it('should fill error array on error in mutaion subscribe', () => {
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
