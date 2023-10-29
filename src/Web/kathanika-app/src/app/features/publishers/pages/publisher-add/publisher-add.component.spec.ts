import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherAddComponent } from './publisher-add.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PanelComponent } from 'src/app/shared/components/panel/panel.component';
import { AlertComponent } from 'src/app/shared/components/alert/alert.component';
import { mockMutationGql } from 'src/test-utils/gql-test-utils';
import { AddPublisherGQL } from 'src/app/graphql/generated/graphql-operations';
import { PublisherFormComponent } from '../../components/publisher-form/publisher-form.component';

describe('PublisherAddComponent', () => {
  let component: PublisherAddComponent;
  let fixture: ComponentFixture<PublisherAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [
        PublisherAddComponent,
        PublisherFormComponent
      ],
      imports: [
        ReactiveFormsModule,
        PanelComponent,
        AlertComponent
      ],
      providers: [
        {
          provide: AddPublisherGQL,
          useValue: mockMutationGql
        }
      ]
    });
    fixture = TestBed.createComponent(PublisherAddComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
