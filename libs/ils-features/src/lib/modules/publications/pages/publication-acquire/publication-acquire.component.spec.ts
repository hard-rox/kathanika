import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { PublicationAcquireComponent } from "./publication-acquire.component";
import {
  AcquirePublicationGQL,
  AcquirePublicationInput,
  AcquisitionMethod,
  PublicationType,
  SearchAuthorsGQL,
  mockMutationGql,
  mockQueryGql,
} from '@kathanika/graphql-ts-client';
import {
  KnPanel,
  KnAlert,
  KnChip,
  KnSearchbar,
} from '@kathanika/kn-ui';
import { AcquirePublicationFormComponent } from '../../components/acquire-publication-form/acquire-publication-form.component';
import { PublicationAuthorsInputComponent } from '../../components/publication-authors-input/publication-authors-input.component';
import { of } from 'rxjs';

describe('PublicationAddComponent', () => {
  let component: PublicationAcquireComponent;
  let fixture: ComponentFixture<PublicationAcquireComponent>;
  const formValue: AcquirePublicationInput = {
    acquisitionMethod: AcquisitionMethod.Purchase,
    authorIds: ['author1', 'author2'],
    callNumber: '123',
    description: 'Description',
    edition: 'First',
    isbn: '1234567890',
    language: 'English',
    publicationType: PublicationType.Book,
    publishedDate: '2023-05-24',
    publisherId: '112233',
    quantity: 1,
    title: 'Title',
    unitPrice: 10.99,
    vendor: 'Vendor',
    coverImageFileId: ''
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [
        PublicationAcquireComponent,
        AcquirePublicationFormComponent,
        PublicationAuthorsInputComponent
      ],
      imports: [
        ReactiveFormsModule,
        KnPanel,
        KnAlert,
        KnChip,
        KnSearchbar
      ],
      providers: [
        {
          provide: AcquirePublicationGQL,
          useValue: mockMutationGql,
        },
        {
          provide: SearchAuthorsGQL,
          useValue: mockQueryGql,
        },
      ],
    });
    fixture = TestBed.createComponent(PublicationAcquireComponent);
    component = fixture.componentInstance;
    //fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should handle form submission with valid data', () => {
    const result = {
      data: {
        acquirePublication: {
          data: { id: '123' },
          message: 'Publication added.',
          errors: null
        }
      }
    };

    jest.spyOn(component['gql'], 'mutate').mockReturnValue(of(result));

    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component['router'], 'navigate');

    component.onValidFormSubmit(formValue);

    expect(component['gql'].mutate).toHaveBeenCalledWith({ acquirePublicationInput: formValue });
    expect(component['alertService'].showToast).toHaveBeenCalledWith('success', 'Publication added.');
    expect(component['router'].navigate).toHaveBeenCalledWith(['/publications/123']);
    expect(component.isPanelLoading).toBe(false);
    expect(component.errors).toEqual([]);
  });

  it('should handle form submission with errors', () => {
    const result = {
      data: {
        acquirePublication: {
          data: null,
          message: null,
          errors: [{ fieldName: 'field1', message: 'field1 - Error message 1' }]
        }
      }
    };

    jest.spyOn(component['gql'], 'mutate').mockReturnValue(of(result));

    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component['router'], 'navigate');

    component.onValidFormSubmit(formValue);

    expect(component['gql'].mutate).toHaveBeenCalledWith({ acquirePublicationInput: formValue });
    expect(component['alertService'].showToast).not.toHaveBeenCalled();
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBe(false);
    expect(component.errors).toEqual(['field1 - Error message 1']);
  });
});
