import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

import { AuthorListComponent } from './author-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { GetAuthorsGQL } from 'src/app/graphql/generated/graphql-operations';

describe('AuthorListComponent', () => {
  let component: AuthorListComponent;
  let fixture: ComponentFixture<AuthorListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AuthorListComponent],
      imports: [SharedModule],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            queryParams: of({}),
          },
        },
        {
          provide: GetAuthorsGQL,
          useValue: {
            watch: () => {
              return {
                valueChanges: {},
                refetch: () => { }
              };
            },
          },
        },
      ],
    })
      .compileComponents()
      .then(() => {
        fixture = TestBed.createComponent(AuthorListComponent);
        component = fixture.componentInstance;
      });
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('changePage should change _queryVariables and call _queryRef.refetch() and setQueryParams()', () => {
    const setQueryParamsSpy = spyOn<any>(component, 'setQueryParams');
    let queryVariables = component['_queryVariables'];
    const queryRefRefetchSpy = spyOn<any>(component['_queryRef'], 'refetch');
    const page = 2;

    component.changePage(page);

    expect(queryVariables.skip).toEqual((page - 1) * queryVariables.take);
    expect(queryRefRefetchSpy).toHaveBeenCalledOnceWith(queryVariables);
    expect(setQueryParamsSpy).toHaveBeenCalled();
  });

  it('changePageSize should change _queryVariables and call _queryRef.refetch() and setQueryParams()', () => {
    const setQueryParamsSpy = spyOn<any>(component, 'setQueryParams');
    let queryVariables = component['_queryVariables'];
    const queryRefRefetchSpy = spyOn<any>(component['_queryRef'], 'refetch');
    const pageSize = 20;

    component.changePageSize(pageSize);

    expect(queryVariables.skip).toEqual(0);
    expect(queryVariables.take).toEqual(pageSize);
    expect(queryRefRefetchSpy).toHaveBeenCalledOnceWith(queryVariables);
    expect(setQueryParamsSpy).toHaveBeenCalled();
  });
});
