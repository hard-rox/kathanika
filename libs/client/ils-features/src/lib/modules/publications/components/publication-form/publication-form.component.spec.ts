import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationFormComponent } from './publication-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SearchAuthorsGQL } from '@kathanika/graphql-consumer';
import { SearchbarModule, ChipComponent } from '@kathanika/kn-ui';
import { mockQueryGql } from '../../../../test-utils/gql-test-utils';

describe('PublicationFormComponent', () => {
  let component: PublicationFormComponent;
  let fixture: ComponentFixture<PublicationFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicationFormComponent],
      imports: [ReactiveFormsModule, SearchbarModule, ChipComponent],
      providers: [
        {
          provide: SearchAuthorsGQL,
          useValue: mockQueryGql,
        },
      ],
    });
    fixture = TestBed.createComponent(PublicationFormComponent);
    component = fixture.componentInstance;
    //fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
