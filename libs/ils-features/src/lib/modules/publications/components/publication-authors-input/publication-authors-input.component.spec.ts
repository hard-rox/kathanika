import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PublicationAuthorsInputComponent } from './publication-authors-input.component';
import { SearchAuthorsGQL } from '@kathanika/graphql-ts-client';
import { mockQueryGql } from '../../../../test-utils/gql-test-utils';
import { KnSearchbarModule } from '@kathanika/kn-ui';
import { NgControl } from '@angular/forms';

describe('PublicationAuthorsInputComponent', () => {
  let component: PublicationAuthorsInputComponent;
  let fixture: ComponentFixture<PublicationAuthorsInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PublicationAuthorsInputComponent],
      imports: [
        KnSearchbarModule
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
});
