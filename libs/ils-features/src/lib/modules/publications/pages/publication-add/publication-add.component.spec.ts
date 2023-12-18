import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationAddComponent } from './publication-add.component';
import { PublicationFormComponent } from '../../components/publication-form/publication-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import {
  AddPublicationGQL,
  SearchAuthorsGQL,
} from '@kathanika/graphql-ts-client';
import {
  KnPanel,
  KnAlert,
  SearchbarModule,
  KnChip,
} from '@kathanika/kn-ui';
import {
  mockMutationGql,
  mockQueryGql,
} from '../../../../test-utils/gql-test-utils';

describe('PublicationAddComponent', () => {
  let component: PublicationAddComponent;
  let fixture: ComponentFixture<PublicationAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicationAddComponent, PublicationFormComponent],
      imports: [
        ReactiveFormsModule,
        KnPanel,
        KnAlert,
        SearchbarModule,
        KnChip,
      ],
      providers: [
        {
          provide: AddPublicationGQL,
          useValue: mockMutationGql,
        },
        {
          provide: SearchAuthorsGQL,
          useValue: mockQueryGql,
        },
      ],
    });
    fixture = TestBed.createComponent(PublicationAddComponent);
    component = fixture.componentInstance;
    //fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
