import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorDetailsComponent } from './author-details.component';
import { of } from 'rxjs';
import { GetAuthorGQL } from 'src/app/graphql/generated';
import { ActivatedRoute } from '@angular/router';

class MockGetAuthorGQL {
  watch = jasmine.createSpy('watch').and.returnValue({
    valueChanges: of({}),
  });
}

describe('AuthorDetailsComponent', () => {
  let component: AuthorDetailsComponent;
  let fixture: ComponentFixture<AuthorDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AuthorDetailsComponent],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: {
                id: '',
              },
            },
          },
        },
        {
          provide: GetAuthorGQL,
          useClass: MockGetAuthorGQL,
        },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(AuthorDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
