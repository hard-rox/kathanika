import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PublicationPatchFormComponent } from './publication-patch-form.component';
import { KnDateInput, KnSelectInput, KnTextInput } from '@kathanika/kn-ui';
import { ReactiveFormsModule } from '@angular/forms';
import { PublicationPublisherInputComponent } from '../publication-publisher-input/publication-publisher-input.component';

describe('PublicationPatchFormComponent', () => {
  let component: PublicationPatchFormComponent;
  let fixture: ComponentFixture<PublicationPatchFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        PublicationPatchFormComponent,
        PublicationPublisherInputComponent
      ],
      imports: [
        ReactiveFormsModule,
        KnSelectInput,
        KnTextInput,
        KnDateInput
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(PublicationPatchFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});
