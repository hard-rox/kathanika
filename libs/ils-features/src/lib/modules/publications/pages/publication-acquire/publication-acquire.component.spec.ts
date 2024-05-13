import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { PublicationAcquireComponent } from "./publication-acquire.component";
import {
  AcquirePublicationGQL,
  SearchAuthorsGQL,
} from '@kathanika/graphql-ts-client';
import {
  KnPanel,
  KnAlert,
  KnSearchbarModule,
  KnChip,
} from '@kathanika/kn-ui';
import {
  mockMutationGql,
  mockQueryGql,
} from '../../../../test-utils/gql-test-utils';
import { AcquirePublicationFormComponent } from '../../components/acquire-publication-form/acquire-publication-form.component';
import { PublicationAuthorsInputComponent } from '../../components/publication-authors-input/publication-authors-input.component';

describe('PublicationAddComponent', () => {
  let component: PublicationAcquireComponent;
  let fixture: ComponentFixture<PublicationAcquireComponent>;

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
        KnSearchbarModule,
        KnChip,
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
});
