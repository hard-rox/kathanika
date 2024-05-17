import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PublicationPatchFormComponent } from './publication-patch-form.component';
import { KnDateInput, KnSelectInput, KnTextInput, KnTextareaInput, KnSearchbarModule } from '@kathanika/kn-ui';
import { PublicationAuthorsInputComponent } from '../publication-authors-input/publication-authors-input.component';
import { NgControl, ReactiveFormsModule } from '@angular/forms';
import { SearchAuthorsGQL } from '@kathanika/graphql-ts-client';
import { mockQueryGql } from '@kathanika/graphql-ts-client';

describe('PublicationPatchFormComponent', () => {
  let component: PublicationPatchFormComponent;
  let fixture: ComponentFixture<PublicationPatchFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        PublicationPatchFormComponent,
        PublicationAuthorsInputComponent
      ],
      imports: [
        KnSelectInput,
        KnTextInput,
        KnDateInput,
        KnTextareaInput,
        KnSearchbarModule,
        ReactiveFormsModule
      ],
      providers: [
        NgControl,
        {
          provide: SearchAuthorsGQL,
          useValue: mockQueryGql
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PublicationPatchFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
