import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherUpdateComponent } from './publisher-update.component';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { PublisherFormComponent } from '../../components/publisher-form/publisher-form.component';
import {
  GetPublisherGQL,
  UpdatePublisherGQL,
} from '@kathanika/graphql-ts-client';
import {
  KnPanel,
  KnAlert,
  KnTextInput,
} from '@kathanika/kn-ui';
import {
  mockQueryGql,
  mockMutationGql,
} from '../../../../test-utils/gql-test-utils';

describe('PublisherUpdateComponent', () => {
  let component: PublisherUpdateComponent;
  let fixture: ComponentFixture<PublisherUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublisherUpdateComponent, PublisherFormComponent],
      imports: [
        RouterTestingModule,
        ReactiveFormsModule,
        KnPanel,
        KnAlert,
        KnTextInput,
      ],
      providers: [
        {
          provide: GetPublisherGQL,
          useValue: mockQueryGql,
        },
        {
          provide: UpdatePublisherGQL,
          useValue: mockMutationGql,
        },
      ],
    });
    fixture = TestBed.createComponent(PublisherUpdateComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
