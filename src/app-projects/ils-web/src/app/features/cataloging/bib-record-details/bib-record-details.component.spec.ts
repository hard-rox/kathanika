import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { BibRecordDetailsComponent } from './bib-record-details.component';
import { BibRecordDetailsGQL } from '../../../graphql/generated/graphql-operations';
import { ApolloTestingModule } from 'apollo-angular/testing';
import { KnBadge, KnButton, KnPanel } from '@kathanika/kn-ui';
import {mockQueryGql} from "../../../graphql/gql-test-utils";

describe('BibRecordDetailsComponent', () => {
  let component: BibRecordDetailsComponent;
  let fixture: ComponentFixture<BibRecordDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        BibRecordDetailsComponent,
        ApolloTestingModule,
        KnPanel,
        KnBadge,
        KnButton
      ],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: { id: 'BIB123' }
            }
          }
        },
        {
          provide: BibRecordDetailsGQL,
          useValue: mockQueryGql
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(BibRecordDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load bib record on init', () => {
    expect(component['queryVariables']).toEqual({ id: 'BIB123' });
  });
});
