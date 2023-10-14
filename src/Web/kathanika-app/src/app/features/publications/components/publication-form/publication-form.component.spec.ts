import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationFormComponent } from './publication-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AutocompleteModule } from 'src/app/shared/modules/autocomplete/autocomplete.module';
import { SearchAuthorsGQL } from 'src/app/graphql/generated/graphql-operations';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';

describe('PublicationFormComponent', () => {
  let component: PublicationFormComponent;
  let fixture: ComponentFixture<PublicationFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicationFormComponent],
      imports: [ReactiveFormsModule, AutocompleteModule],
      providers: [
        {
          provide: SearchAuthorsGQL,
          useValue: mockQueryGql,
        }
      ]
    });
    fixture = TestBed.createComponent(PublicationFormComponent);
    component = fixture.componentInstance;
    //fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
