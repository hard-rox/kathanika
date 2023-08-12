import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationDetailsComponent } from './publication-details.component';
import { GetPublicationGQL } from 'src/app/graphql/generated/graphql-operations';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';
import { ActivatedRoute } from '@angular/router';

describe('PublicationDetailsComponent', () => {
  let component: PublicationDetailsComponent;
  let fixture: ComponentFixture<PublicationDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicationDetailsComponent],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: {
                id: '12345',
              },
            },
          },
        },
        {
          provide: GetPublicationGQL,
          useValue: mockQueryGql,
        },
      ]
    });
    fixture = TestBed.createComponent(PublicationDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
