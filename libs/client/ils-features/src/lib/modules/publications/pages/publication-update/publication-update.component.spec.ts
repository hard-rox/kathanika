import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationUpdateComponent } from './publication-update.component';
import {
  GetPublicationGQL,
  SearchAuthorsGQL,
  UpdatePublicationGQL,
} from '@kathanika/graphql-consumer';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import {
  PanelComponent,
  AlertComponent,
  TextInputComponent,
  SelectInputComponent,
  DateInputComponent,
  TextareaInputComponent,
  NumberInputComponent,
  SearchbarModule,
  ChipComponent,
} from '@kathanika/kn-ui';
import {
  mockQueryGql,
  mockMutationGql,
} from '../../../../test-utils/gql-test-utils';
import { PublicationFormComponent } from '../../components/publication-form/publication-form.component';

describe('PublicationUpdateComponent', () => {
  let component: PublicationUpdateComponent;
  let fixture: ComponentFixture<PublicationUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicationUpdateComponent, PublicationFormComponent],
      imports: [
        RouterTestingModule,
        ReactiveFormsModule,
        PanelComponent,
        AlertComponent,
        TextInputComponent,
        SelectInputComponent,
        DateInputComponent,
        TextareaInputComponent,
        NumberInputComponent,
        SearchbarModule,
        ChipComponent,
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
