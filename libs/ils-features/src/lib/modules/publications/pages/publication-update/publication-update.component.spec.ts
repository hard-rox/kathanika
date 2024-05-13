import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationUpdateComponent } from './publication-update.component';
import {
  GetPublicationGQL,
  SearchAuthorsGQL,
  UpdatePublicationGQL,
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
  KnSearchbarModule,
  KnChip,
} from '@kathanika/kn-ui';
import {
  mockQueryGql,
  mockMutationGql,
} from '../../../../test-utils/gql-test-utils';
import { PublicationPatchFormComponent } from '../../components/publication-patch-form/publication-patch-form.component';
import { RouterModule } from '@angular/router';
import { PublicationAuthorsInputComponent } from '../../components/publication-authors-input/publication-authors-input.component';

describe('PublicationUpdateComponent', () => {
  let component: PublicationUpdateComponent;
  let fixture: ComponentFixture<PublicationUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicationUpdateComponent, PublicationPatchFormComponent, PublicationAuthorsInputComponent],
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
        KnSearchbarModule,
        KnChip,
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
      ],
    });
    fixture = TestBed.createComponent(PublicationUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
