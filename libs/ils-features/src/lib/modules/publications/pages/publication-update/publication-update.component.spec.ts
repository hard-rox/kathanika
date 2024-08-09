import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationUpdateComponent } from './publication-update.component';
import {
  GetPublicationGQL,
  GetPublicationQuery,
  PublicationPatchInput,
  PublicationType,
  SearchAuthorsGQL,
  SearchPublishersGQL,
  UpdatePublicationGQL,
  UpdatePublicationMutation,
  mockMutationGql,
  mockQueryGql,
} from '@kathanika/graphql-ts-client';
import { ReactiveFormsModule } from '@angular/forms';
import {
  KnPanel,
  KnAlert,
  KnTextInput,
  KnSelectInput,
  KnDateInput,
  KnTextareaInput,
  KnNumberInput,
  KnChip,
  KnSearchbar,
  KnFileInput,
  FileServerModule,
} from '@kathanika/kn-ui';
import { PublicationPatchFormComponent } from '../../components/publication-patch-form/publication-patch-form.component';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { PublicationAuthorsInputComponent } from '../../components/publication-authors-input/publication-authors-input.component';
import { ApolloQueryResult } from '@apollo/client';
import { of, throwError } from 'rxjs';
import { MutationResult } from 'apollo-angular';
import { PublicationPublisherInputComponent } from '../../components/publication-publisher-input/publication-publisher-input.component';

describe('PublicationUpdateComponent', () => {
  let component: PublicationUpdateComponent;
  let fixture: ComponentFixture<PublicationUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [
        PublicationUpdateComponent,
        PublicationPatchFormComponent,
        PublicationAuthorsInputComponent,
        PublicationPublisherInputComponent
      ],
      imports: [
        RouterModule.forRoot([]),
        ReactiveFormsModule,
        KnPanel,
        KnAlert,
        KnTextInput,
        KnSelectInput,
        KnDateInput,
        KnTextareaInput,
        KnNumberInput,
        KnChip,
        KnSearchbar,
        FileServerModule.forRoot(''),
        KnFileInput
      ],
      providers: [
        {
          provide: GetPublicationGQL,
          useValue: mockQueryGql,
        },
        {
          provide: UpdatePublicationGQL,
          useValue: mockMutationGql,
        },
        {
          provide: SearchAuthorsGQL,
          useValue: mockQueryGql,
        },
        {
          provide: SearchPublishersGQL,
          useValue: mockQueryGql
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
    TestBed.inject(ActivatedRoute);
    fixture = TestBed.createComponent(PublicationUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load publication data on component initialization', () => {
    const mockQueryResult: ApolloQueryResult<GetPublicationQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        publication: {
          id: '123',
          title: 'Example Title',
          publicationType: PublicationType.Book, // Replace PublicationType with the actual enum type
          isbn: '978-3-16-148410-0',
          edition: '1st Edition',
          callNumber: 'ABC123',
          language: 'English',
          publisher: {
            id: '11222',
            name: 'Hello world'
          },
          publishedDate: new Date(),
          copiesAvailable: 10,
          description: 'Description of the publication',
          authors: [
            {
              id: 'author1',
              firstName: 'John',
              lastName: 'Doe',
              fullName: 'John Doe'
            }
          ],
          purchaseRecords: [
            {
              id: 'purchase1',
              purchasedDate: new Date(),
              vendor: 'Vendor',
              quantity: 1,
              unitPrice: 10.99,
              totalPrice: 10.99
            }
          ],
          donationRecords: [
            {
              id: 'donation1',
              donationDate: new Date(),
              patron: 'Patron',
              quantity: 1
            }
          ]
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
    expect(component.publication).toEqual(mockQueryResult.data.publication);
  });

  it('should handle error when loading publication data', () => {
    const mockQueryResult: ApolloQueryResult<GetPublicationQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        publication: null
      },
      error: {
        cause: null,
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
    expect(component.publication).toBeUndefined();
  });

  it('should handle publication not found', () => {
    const mockQueryResult: ApolloQueryResult<GetPublicationQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        publication: null
      }
    };
    jest.spyOn(component['gql'], 'fetch').mockReturnValueOnce(of(mockQueryResult));

    jest.spyOn(component['alertService'], 'showPopup');
    jest.spyOn(component['router'], 'navigate');

    component.ngOnInit();

    expect(component['gql'].fetch).toHaveBeenCalledWith({ id: '123' });
    expect(component['alertService'].showPopup).toHaveBeenCalledWith('error', 'Returning to list page.', 'Publication not found');
    expect(component['router'].navigate).toHaveBeenCalledWith(['/publications']);
    expect(component.publication).toBeUndefined();
  });

  it('should handle form submission with successful result', () => {
    const result = {
      data: {
        updatePublication: {
          data: { id: '123' },
          message: 'Publication updated.',
          errors: null
        }
      }
    };

    jest.spyOn(component['mutation'], 'mutate').mockReturnValueOnce(of(result));
    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component.publicationUpdateForm, 'resetForm');
    jest.spyOn(component['router'], 'navigate');

    component.publicationId = '123';
    const publicationOutput = {} as PublicationPatchInput;

    component.onValidFormSubmit(publicationOutput);

    expect(component['mutation'].mutate).toHaveBeenCalledWith({
      id: '123',
      publicationPatch: publicationOutput
    });
    expect(component['alertService'].showToast).toHaveBeenCalledWith('success', 'Publication updated.');
    expect(component.publicationUpdateForm.resetForm).toHaveBeenCalled();
    expect(component['router'].navigate).toHaveBeenCalledWith(['/publications/123']);
  });

  it('should handle form submission with errors', () => {
    const result: MutationResult<UpdatePublicationMutation> = {
      data: {
        updatePublication: {
          data: null,
          message: null,
          errors: [
            { __typename: 'ValidationError', fieldName: 'fieldName1', message: 'Invalid field 1' },
          ]
        }
      }
    };

    jest.spyOn(component['mutation'], 'mutate').mockReturnValueOnce(of(result));
    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component.publicationUpdateForm, 'resetForm');
    jest.spyOn(component['router'], 'navigate');

    component.publicationId = '123';
    const publicationOutput = {} as PublicationPatchInput;

    component.onValidFormSubmit(publicationOutput);

    expect(component['mutation'].mutate).toHaveBeenCalledWith({
      id: '123',
      publicationPatch: publicationOutput
    });
    expect(component['alertService'].showToast).not.toHaveBeenCalled();
    expect(component.publicationUpdateForm.resetForm).not.toHaveBeenCalled();
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBe(false);
    expect(component.errors.length).toEqual(1);
  });

  it('should handle form submission with network error', () => {
    jest.spyOn(component['mutation'], 'mutate')
      .mockReturnValueOnce(throwError(() => new Error('Network error')));
    jest.spyOn(component['alertService'], 'showPopup');
    jest.spyOn(component['router'], 'navigate');

    component.publicationId = '123';
    const publicationOutput = {} as PublicationPatchInput;

    component.onValidFormSubmit(publicationOutput);

    expect(component['mutation'].mutate).toHaveBeenCalledWith({
      id: '123',
      publicationPatch: publicationOutput
    });
    expect(component['alertService'].showPopup).toHaveBeenCalledWith('error', 'Network error');
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBe(false);
  });
});
