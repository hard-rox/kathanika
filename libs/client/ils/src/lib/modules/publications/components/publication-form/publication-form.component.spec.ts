import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationFormComponent } from './publication-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { SearchAuthorsGQL } from '@kathanika/graphql-consumer';
import { mockQueryGql } from 'src/test-utils/gql-test-utils';
import { SearchbarModule } from 'src/app/shared/modules/searchbar/searchbar.module';
import { ChipComponent } from 'src/app/shared/components/chip/chip.component';

describe('PublicationFormComponent', () => {
  let component: PublicationFormComponent;
  let fixture: ComponentFixture<PublicationFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicationFormComponent],
      imports: [
        ReactiveFormsModule,
        SearchbarModule,
        ChipComponent
      ],
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
