import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PublicationAuthorsInputComponent } from './publication-authors-input.component';
import { SearchAuthorsGQL, SearchAuthorsQuery, SearchAuthorsQueryVariables, mockQueryGql } from '@kathanika/graphql-ts-client';
import { KnChip, KnSearchbar } from '@kathanika/kn-ui';
import { NgControl } from '@angular/forms';
import { By } from '@angular/platform-browser';
import { ApolloQueryResult, NetworkStatus } from '@apollo/client';

describe('PublicationAuthorsInputComponent', () => {
  let component: PublicationAuthorsInputComponent;
  let fixture: ComponentFixture<PublicationAuthorsInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PublicationAuthorsInputComponent],
      imports: [
        KnChip,
        KnSearchbar
      ],
      providers: [
      NgControl,
        {
          provide: SearchAuthorsGQL,
          useValue: mockQueryGql
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PublicationAuthorsInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should trigger actionPerformed event when kn-chip is clicked', () => {
    component.currentAuthors = [{
      id: '1',
      fullName: 'Hello World',
      firstName: 'Hello',
      lastName: 'World'
    },{
      id: '2',
      fullName: 'Hello World',
      firstName: 'Hello',
      lastName: 'World'
    }]
    fixture.detectChanges();
    const chipElements = fixture.debugElement.queryAll(By.directive(KnChip));
    chipElements[0].triggerEventHandler('actionPerformed', '1');
    fixture.detectChanges();

    expect(component['selectedAuthors'].length).toEqual(1);
  });

  it('should call filter with correct query variables when search text changes', () => {
    const filterText = 'search text';
    const queryResult: ApolloQueryResult<SearchAuthorsQuery> = {
      data: {
        authors: {
          items: []
        }
      },
      loading: false,
      networkStatus: NetworkStatus.ready
    }

    jest.spyOn(component['authorSearchQueryRef'], 'refetch').mockResolvedValue(queryResult);

    const searchBarElement = fixture.debugElement.query(By.css('kn-searchbar'));
    searchBarElement.triggerEventHandler('searchTextChanged', filterText);

    const expectedVariables: SearchAuthorsQueryVariables = {
      filterText: filterText,
    };
    expect(component['authorSearchQueryRef'].refetch).toHaveBeenCalledWith(expectedVariables);
  });

  it('should update value and selected authors when result gets selected', () => {
    const queryResult: ApolloQueryResult<SearchAuthorsQuery> = {
      data: {
        authors: {
          items: [{
            id: '1',
            fullName: 'Hello World'
          },{
            id: '2',
            fullName: 'Hello World 2'
          }]
        }
      },
      loading: false,
      networkStatus: NetworkStatus.ready
    }

    jest.spyOn(component['authorSearchQueryRef'], 'refetch').mockResolvedValueOnce(queryResult);

    const searchBarElement = fixture.debugElement.query(By.css('kn-searchbar'));
    searchBarElement.triggerEventHandler('searchTextChanged', '');
    searchBarElement.triggerEventHandler('resultSelected', queryResult.data.authors?.items?.at(0));

    expect(component['selectedAuthors'].length).toEqual(1);
  });
});
