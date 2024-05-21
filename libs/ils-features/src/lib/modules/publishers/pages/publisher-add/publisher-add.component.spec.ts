import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherAddComponent } from './publisher-add.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddPublisherGQL, AddPublisherInput, AddPublisherMutation } from '@kathanika/graphql-ts-client';
import { PublisherFormComponent } from '../../components/publisher-form/publisher-form.component';
import { KnPanel, KnAlert, KnTextInput, KnTextareaInput } from '@kathanika/kn-ui';
import { mockMutationGql } from '@kathanika/graphql-ts-client';
import { of } from 'rxjs';
import { MutationResult } from 'apollo-angular';

describe('PublisherAddComponent', () => {
  let component: PublisherAddComponent;
  let fixture: ComponentFixture<PublisherAddComponent>;
  const formValue: AddPublisherInput = {
    name: 'Test'
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublisherAddComponent, PublisherFormComponent],
      imports: [
        ReactiveFormsModule,
        KnPanel,
        KnAlert,
        KnTextInput,
        KnTextareaInput
      ],
      providers: [
        {
          provide: AddPublisherGQL,
          useValue: mockMutationGql,
        },
      ],
    });
    fixture = TestBed.createComponent(PublisherAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should handle form submission with valid data', () => {
    const result: MutationResult<AddPublisherMutation> = {
      data: {
        addPublisher: {
          data: { id: '123' },
          message: 'Publisher added.',
          errors: null
        }
      }
    };

    jest.spyOn(component['gql'], 'mutate').mockReturnValue(of(result));

    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component['router'], 'navigate');

    component.onValidFormSubmit(formValue);

    expect(component['gql'].mutate).toHaveBeenCalledWith({ addPublisherInput: formValue });
    expect(component['alertService'].showToast).toHaveBeenCalledWith('success', 'Publisher added.');
    expect(component['router'].navigate).toHaveBeenCalledWith(['/publishers/123']);
    expect(component.isPanelLoading).toBe(false);
    expect(component.errors).toEqual([]);
  });

  it('should handle form submission with errors', () => {
    const result: MutationResult<AddPublisherMutation> = {
      data: {
        addPublisher: {
          data: null,
          message: null,
          errors: [{ __typename: 'InvalidFieldError', fieldName: 'field1', message: 'Error message 1' }]
        }
      }
    };

    jest.spyOn(component['gql'], 'mutate').mockReturnValue(of(result));

    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component['router'], 'navigate');

    component.onValidFormSubmit(formValue);

    expect(component['gql'].mutate).toHaveBeenCalledWith({ addPublisherInput: formValue });
    expect(component['alertService'].showToast).not.toHaveBeenCalled();
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBe(false);
    expect(component.errors).toEqual(['field1 - Error message 1']);
  });
});
