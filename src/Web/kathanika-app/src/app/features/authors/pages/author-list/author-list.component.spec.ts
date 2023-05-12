import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';

import { AuthorListComponent } from './author-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { GetAuthorsGQL } from 'src/app/graphql/generated/graphql-operations';

class MockGetAuthorsGQL {
  watch = jasmine.createSpy('watch').and.returnValue({
    valueChanges: of({
      data: {
        authors: {
          items: [],
        },
      },
    }),
  });
}

describe('AuthorListComponent', () => {
  let component: AuthorListComponent;
  let fixture: ComponentFixture<AuthorListComponent>;
  let gql: MockGetAuthorsGQL;

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
          useClass: MockGetAuthorsGQL,
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(AuthorListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  xit('watch should be called once', () => {
    expect(gql.watch).toHaveBeenCalled();
  });
});
