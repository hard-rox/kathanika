import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationAddComponent } from './publication-acquire.component';
import { ReactiveFormsModule } from '@angular/forms';
import {
  AcquirePublicationGQL,
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
import { AcquirePublicationFormComponent } from '../../components/acquire-publication-form/acquire-publication-form.component';

describe('PublicationAddComponent', () => {
  let component: PublicationAddComponent;
  let fixture: ComponentFixture<PublicationAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicationAddComponent, AcquirePublicationFormComponent],
      imports: [
        ReactiveFormsModule,
        KnPanel,
        KnAlert,
        SearchbarModule,
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
    fixture = TestBed.createComponent(PublicationAddComponent);
    component = fixture.componentInstance;
    //fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
