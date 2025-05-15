import { ComponentFixture, TestBed } from '@angular/core/testing';
import { mockQueryGql } from '../../../graphql/gql-test-utils';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import {BibRecordListGQL} from '../../../graphql/generated/graphql-operations';
import { BibRecordListComponent } from './bib-record-list.component';

// Helper to access private/protected for test
// eslint-disable-next-line @typescript-eslint/no-explicit-any
function get(obj: unknown, prop: string): any {
  return (obj as any)[prop];
}

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
    get(component, 'queryVariables').skip = 0;
    get(component, 'queryVariables').take = 10;
    get(component, 'setSearchTextQueryFilter').call(component, '');
    expect(get(component, 'queryVariables').filter).toBe(null);
  });

  it('should set filter with OR conditions if searchText is provided', () => {
    get(component, 'queryVariables').skip = 0;
    get(component, 'queryVariables').take = 10;
    get(component, 'setSearchTextQueryFilter').call(component, 'test');
    expect(get(component, 'queryVariables').filter).not.toBe(null);
    expect(get(component, 'queryVariables').filter).not.toBe(undefined);
    expect((get(component, 'queryVariables').filter?.or ?? []).length).toBeGreaterThan(0);
  });

  it('should call init on ngOnInit', () => {
    // Patch the protected method for spying
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    const originalInit = (component as any).init;
    let called = false;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    (component as any).init = () => { called = true; };
    component.ngOnInit();
    expect(called).toBe(true);
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    (component as any).init = originalInit;
  });

  xit('should update searchText and filter on searchTextSubject emission', () => {
    get(component, 'queryVariables').skip = 0;
    get(component, 'queryVariables').take = 10;
    get(component, 'searchText');
    const setSearchTextQueryFilter = jest.fn();
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    (component as any).setSearchTextQueryFilter = setSearchTextQueryFilter;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    (component as any).queryRef = { refetch: () => undefined };
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    (component as any).setQueryParams = () => undefined;
    get(component, 'searchTextSubject').next('abc');
    expect(setSearchTextQueryFilter.mock.calls.some(call => call[0] === 'abc')).toBe(true);
  });
});
