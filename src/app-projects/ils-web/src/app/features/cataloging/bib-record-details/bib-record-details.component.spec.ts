import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { BibRecordDetailsComponent } from './bib-record-details.component';
import { BibRecordDetailsGQL } from '../../../graphql/generated/graphql-operations';
import { ApolloTestingModule } from 'apollo-angular/testing';
import { of } from 'rxjs';
import { KnBadge, KnButton, KnPanel } from '@kathanika/kn-ui';

describe('BibRecordDetailsComponent', () => {
  let component: BibRecordDetailsComponent;
  let fixture: ComponentFixture<BibRecordDetailsComponent>;
  let gqlService: BibRecordDetailsGQL;

  const mockBibRecord = {
    id: 'BIB123',
    title: 'Test Book',
    author: 'Test Author',
    isbn: '1234567890',
    publicationYear: '2023',
    status: 'Available',
    marcFields: [
      {
        tag: '245',
        indicators: '10',
        subfields: '$a',
        value: 'Test Book'
      }
    ],
    holdings: [
      {
        id: 'HOLD123',
        location: 'Main Library',
        callNumber: 'TEST 123',
        status: 'Available',
        dueDate: null
      }
    ]
  };

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
          useValue: {
            watch: jest.fn().mockReturnValue({
              valueChanges: of({
                loading: false,
                data: {
                  bibRecord: mockBibRecord
                }
              })
            })
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(BibRecordDetailsComponent);
    component = fixture.componentInstance;
    gqlService = TestBed.inject(BibRecordDetailsGQL);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load bib record on init', () => {
    expect(component['queryVariables']).toEqual({ id: 'BIB123' });
  });

  it('should display bib record details', () => {
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    
    expect(compiled.querySelector('h5')?.textContent).toContain('Record ID:');
    expect(compiled.querySelector('p')?.textContent).toContain('BIB123');
  });

  it('should show available status with success badge', () => {
    fixture.detectChanges();
    const badge = fixture.nativeElement.querySelector('kn-badge');
    expect(badge.getAttribute('ng-reflect-content')).toBe('Available');
    expect(badge.getAttribute('ng-reflect-type')).toBe('success');
  });

  it('should display MARC fields in table', () => {
    fixture.detectChanges();
    const table = fixture.nativeElement.querySelector('table');
    const rows = table?.querySelectorAll('tbody tr');
    expect(rows?.length).toBe(1);
    expect(rows?.[0].querySelector('td')?.textContent).toContain('245');
  });

  it('should display holdings information', () => {
    fixture.detectChanges();
    const tables = fixture.nativeElement.querySelectorAll('table');
    const holdingsTable = tables[1];
    const rows = holdingsTable?.querySelectorAll('tbody tr');
    expect(rows?.length).toBe(1);
    expect(rows?.[0].querySelector('td')?.textContent).toContain('HOLD123');
  });
});
