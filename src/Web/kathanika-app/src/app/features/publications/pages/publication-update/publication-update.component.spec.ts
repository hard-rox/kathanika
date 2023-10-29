import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationUpdateComponent } from './publication-update.component';
import { GetPublicationGQL, SearchAuthorsGQL, UpdatePublicationGQL } from 'src/app/graphql/generated/graphql-operations';
import { mockMutationGql, mockQueryGql } from 'src/test-utils/gql-test-utils';
import { RouterTestingModule } from '@angular/router/testing';
import { PanelComponent } from 'src/app/shared/components/panel/panel.component';
import { AlertComponent } from 'src/app/shared/components/alert/alert.component';
import { PublicationFormComponent } from '../../components/publication-form/publication-form.component';
import { TextInputComponent } from 'src/app/shared/components/text-input/text-input.component';
import { SelectInputComponent } from 'src/app/shared/components/select-input/select-input.component';
import { DateInputComponent } from 'src/app/shared/components/date-input/date-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TextareaInputComponent } from 'src/app/shared/components/textarea-input/textarea-input.component';
import { NumberInputComponent } from 'src/app/shared/components/number-input/number-input.component';
import { SearchbarModule } from 'src/app/shared/modules/searchbar/searchbar.module';
import { ChipComponent } from 'src/app/shared/components/chip/chip.component';

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
        ChipComponent
      ],
      providers: [
        {
          provide: GetPublicationGQL,
          useValue: mockQueryGql
        },
        {
          provide: UpdatePublicationGQL,
          useValue: mockMutationGql
        },
        {
          provide: SearchAuthorsGQL,
          useValue: mockQueryGql,
        }
      ]
    });
    fixture = TestBed.createComponent(PublicationUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
