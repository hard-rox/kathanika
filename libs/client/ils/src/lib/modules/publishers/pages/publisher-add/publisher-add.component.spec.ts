import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherAddComponent } from './publisher-add.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddPublisherGQL } from '@kathanika/graphql-consumer';
import { PublisherFormComponent } from '../../components/publisher-form/publisher-form.component';
import { PanelComponent, AlertComponent } from '@kathanika/kn-ui';
import { mockMutationGql } from '../../../../test-utils/gql-test-utils';

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
