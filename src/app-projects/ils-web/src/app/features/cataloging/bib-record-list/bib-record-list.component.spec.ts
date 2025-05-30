import { ComponentFixture, TestBed } from '@angular/core/testing';
import { mockQueryGql } from '../../../graphql/gql-test-utils';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import {BibRecordListGQL} from '../../../graphql/generated/graphql-operations';
import { BibRecordListComponent } from './bib-record-list.component';

describe('BibRecordListComponent', () => {
  let component: BibRecordListComponent;
  let fixture: ComponentFixture<BibRecordListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BibRecordListComponent],
      providers: [
          {
              provide: ActivatedRoute,
              useValue: {
                  queryParams: of({
                      size: 20,
                      page: 2,
                  }),
              },
          },
          {
              provide: BibRecordListGQL,
              useValue: mockQueryGql
          }
      ]
    }).compileComponents();
    fixture = TestBed.createComponent(BibRecordListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set filter to null if searchText is empty', () => {
    component['queryVariables'].skip = 0;
    component['queryVariables'].take = 10;
    component['setSearchTextQueryFilter'].call(component, '');
    expect(component['queryVariables'].filter).toBe(null);
  });

  it('should set filter with OR conditions if searchText is provided', () => {
    component['queryVariables'].skip = 0;
    component['queryVariables'].take = 10;
    component['setSearchTextQueryFilter'].call(component, 'test');
    expect(component['queryVariables'].filter).not.toBe(null);
    expect(component['queryVariables'].filter).not.toBe(undefined);
    expect((component['queryVariables'].filter?.or ?? []).length).toBeGreaterThan(0);
  });

  it('should call init on ngOnInit', () => {
    // Patch the protected method for spying
    const originalInit = component['init'];
    let called = false;
    component['init'] = () => { called = true; };
    component.ngOnInit();
    expect(called).toBe(true);
    component['init'] = originalInit;
  });

  xit('should update searchText and filter on searchTextSubject emission', () => {
    component['queryVariables'].skip = 0;
    component['queryVariables'].take = 10;
    component['searchText'] = '';
    const setSearchTextQueryFilter = jest.fn();
    component['setSearchTextQueryFilter'] = setSearchTextQueryFilter;
    component['setQueryParams'] = () => undefined;
    component['searchTextSubject'].next('abc');
    expect(setSearchTextQueryFilter.mock.calls.some(call => call[0] === 'abc')).toBe(true);
  });
});
