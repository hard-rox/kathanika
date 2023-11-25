import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherUpdateComponent } from './publisher-update.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { PanelComponent } from 'src/app/shared/components/panel/panel.component';
import { GetPublisherGQL, UpdatePublisherGQL } from '@kathanika/graphql-consumer';
import { AlertComponent } from 'src/app/shared/components/alert/alert.component';
import { TextInputComponent } from 'src/app/shared/components/text-input/text-input.component';
import { mockQueryGql, mockMutationGql } from 'src/test-utils/gql-test-utils';
import { PublisherFormComponent } from '../../components/publisher-form/publisher-form.component';

describe('PublisherUpdateComponent', () => {
  let component: PublisherUpdateComponent;
  let fixture: ComponentFixture<PublisherUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublisherUpdateComponent, PublisherFormComponent],
      imports: [
        RouterTestingModule,
        ReactiveFormsModule,
        PanelComponent,
        AlertComponent,
        TextInputComponent
      ],
      providers: [
        {
          provide: GetPublisherGQL,
          useValue: mockQueryGql
        },
        {
          provide: UpdatePublisherGQL,
          useValue: mockMutationGql
        }
      ]
    });
    fixture = TestBed.createComponent(PublisherUpdateComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
