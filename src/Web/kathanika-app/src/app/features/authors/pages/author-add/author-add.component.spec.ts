import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorAddComponent } from './author-add.component';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddAuthorGQL } from 'src/app/graphql/generated/graphql-operations';
import { mockMutatuionGql } from 'src/test-utils/gql-test-utils';
import { PanelComponent } from 'src/app/shared/modules/panel/components/panel/panel.component';

describe('AuthorAddComponent', () => {
  let component: AuthorAddComponent;
  let fixture: ComponentFixture<AuthorAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule
      ],
      declarations: [
        AuthorAddComponent,
        AuthorFormComponent,
        PanelComponent
      ],
      providers: [
        {
          provide: AddAuthorGQL,
          useValue: mockMutatuionGql,
        },
      ]
    });
    fixture = TestBed.createComponent(AuthorAddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
