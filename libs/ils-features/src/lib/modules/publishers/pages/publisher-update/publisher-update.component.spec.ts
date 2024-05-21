import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherUpdateComponent } from './publisher-update.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PublisherFormComponent } from '../../components/publisher-form/publisher-form.component';
import {
  GetPublisherGQL,
  GetPublisherQuery,
  PublisherPatchInput,
  UpdatePublisherGQL,
  UpdatePublisherMutation,
  mockMutationGql,
  mockQueryGql,
} from '@kathanika/graphql-ts-client';
import {
  KnPanel,
  KnAlert,
  KnTextInput,
  KnTextareaInput,
} from '@kathanika/kn-ui';
import { ApolloQueryResult } from '@apollo/client';
import { of, throwError } from 'rxjs';
import { MutationResult } from 'apollo-angular';
import { ActivatedRoute, RouterModule } from '@angular/router';

describe('PublisherUpdateComponent', () => {
  let component: PublisherUpdateComponent;
  let fixture: ComponentFixture<PublisherUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublisherUpdateComponent, PublisherFormComponent],
      imports: [
        RouterModule.forRoot([]),
        ReactiveFormsModule,
        KnPanel,
        KnAlert,
        KnTextInput,
        KnTextareaInput
      ],
      providers: [
        {
          provide: GetPublisherGQL,
          useValue: mockQueryGql,
        },
        {
          provide: UpdatePublisherGQL,
          useValue: mockMutationGql,
        },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: {
                id: '123',
              },
            },
          },
        },
      ],
    });
    fixture = TestBed.createComponent(PublisherUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load publisher data on component initialization', () => {
    const mockQueryResult: ApolloQueryResult<GetPublisherQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        publisher: {
          id: '123',
          name: ''
        },
      },
    };

    jest.spyOn(component['gql'], 'fetch').mockReturnValueOnce(of(mockQueryResult));

    jest.spyOn(component['alertService'], 'showPopup');
    jest.spyOn(component['router'], 'navigate');

    component.ngOnInit();

    expect(component['gql'].fetch).toHaveBeenCalledWith({ id: '123' });
    expect(component['alertService'].showPopup).not.toHaveBeenCalled();
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBeFalsy();
    expect(component.publisherToUpdate).toEqual(mockQueryResult.data.publisher);
  });

  it('should handle error when loading publisher data', () => {
    const mockQueryResult: ApolloQueryResult<GetPublisherQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        publisher: undefined
      },
      error: {
        graphQLErrors: [],
        protocolErrors: [],
        clientErrors: [],
        networkError: null,
        message: 'An error occurred',
        name: 'Test',
        extraInfo: { additionalData: 'Additional information' },
      }
    };

    jest.spyOn(component['gql'], 'fetch').mockReturnValueOnce(of(mockQueryResult));

    jest.spyOn(component['alertService'], 'showPopup');
    jest.spyOn(component['router'], 'navigate');

    component.ngOnInit();

    expect(component['gql'].fetch).toHaveBeenCalledWith({ id: '123' });
    expect(component['alertService'].showPopup).toHaveBeenCalledWith('error', mockQueryResult.error?.message);
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.publisherToUpdate).toBeUndefined();
  });

  it('should handle publisher not found', () => {
    const mockQueryResult: ApolloQueryResult<GetPublisherQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        publisher: null
      }
    };
    jest.spyOn(component['gql'], 'fetch').mockReturnValueOnce(of(mockQueryResult));

    jest.spyOn(component['alertService'], 'showPopup');
    jest.spyOn(component['router'], 'navigate');

    component.ngOnInit();

    expect(component['gql'].fetch).toHaveBeenCalledWith({ id: '123' });
    expect(component['alertService'].showPopup).toHaveBeenCalledWith('error', 'Returning to list page.', 'Publisher not found');
    expect(component['router'].navigate).toHaveBeenCalledWith(['/publishers']);
    expect(component.publisherToUpdate).toBeUndefined();
  });

  it('should handle form submission with successful result', () => {
    const result = {
      data: {
        updatePublisher: {
          data: { id: '123' },
          message: 'Publisher updated.',
          errors: null
        }
      }
    };

    jest.spyOn(component['mutation'], 'mutate').mockReturnValueOnce(of(result));
    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component.publisherUpdateForm, 'resetForm');
    jest.spyOn(component['router'], 'navigate');

    component.publisherId = '123';
    const publisherOutput = {} as PublisherPatchInput;

    component.onValidFormSubmit(publisherOutput);

    expect(component['mutation'].mutate).toHaveBeenCalledWith({
      id: '123',
      publisherPatch: publisherOutput
    });
    expect(component['alertService'].showToast).toHaveBeenCalledWith('success', 'Publisher updated.');
    expect(component.publisherUpdateForm.resetForm).toHaveBeenCalled();
    expect(component['router'].navigate).toHaveBeenCalledWith(['/publishers/123']);
  });

  it('should handle form submission with errors', () => {
    const result: MutationResult<UpdatePublisherMutation> = {
      data: {
        updatePublisher: {
          data: null,
          message: null,
          errors: [
            { __typename: 'InvalidFieldError', fieldName: 'fieldName1', message: 'Invalid field 1' },
            { __typename: 'NotFoundWithTheIdError', id: '123', objectName: 'objectName1', message: 'Object not found 1' },
          ]
        }
      }
    };

    jest.spyOn(component['mutation'], 'mutate').mockReturnValueOnce(of(result));
    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component.publisherUpdateForm, 'resetForm');
    jest.spyOn(component['router'], 'navigate');

    component.publisherId = '123';
    const publisherOutput = {} as PublisherPatchInput;

    component.onValidFormSubmit(publisherOutput);

    expect(component['mutation'].mutate).toHaveBeenCalledWith({
      id: '123',
      publisherPatch: publisherOutput
    });
    expect(component['alertService'].showToast).not.toHaveBeenCalled();
    expect(component.publisherUpdateForm.resetForm).not.toHaveBeenCalled();
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBe(false);
    expect(component.errors.length).toEqual(2);
  });

  it('should handle form submission with network error', () => {
    const error = new Error('Network error');
    jest.spyOn(component['mutation'], 'mutate')
      .mockReturnValueOnce(throwError(() => error));
    jest.spyOn(component['alertService'], 'showHttpErrorPopup');
    jest.spyOn(component['router'], 'navigate');

    component.publisherId = '123';
    const publisherOutput = {} as PublisherPatchInput;

    component.onValidFormSubmit(publisherOutput);

    expect(component['mutation'].mutate).toHaveBeenCalledWith({
      id: '123',
      publisherPatch: publisherOutput
    });
    expect(component['alertService'].showHttpErrorPopup).toHaveBeenCalledWith(error);
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBe(false);
  });
});
