import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorUpdateComponent } from './author-update.component';
import { ActivatedRoute } from '@angular/router';
import { GetAuthorGQL, UpadateAuthorGQL } from 'src/app/graphql/generated/graphql-operations';
import { mockMutatuionGql, mockQueryGql } from 'src/test-utils/gql-test-utils';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';

describe('AuthorUpdateComponent', () => {
  let component: AuthorUpdateComponent;
  let fixture: ComponentFixture<AuthorUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      declarations: [AuthorFormComponent, AuthorUpdateComponent],
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
          provide: GetAuthorGQL,
          useValue: mockQueryGql,
        },
        {
          provide: UpadateAuthorGQL,
          useValue: mockMutatuionGql,
        },
      ],
    });
    fixture = TestBed.createComponent(AuthorUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
