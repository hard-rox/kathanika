import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { PublicationFormInput } from '../../types/publication-form-input';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PublicationFormOutput } from '../../types/publication-form-output';

@Component({
  selector: 'kn-publication-form',
  templateUrl: './publication-form.component.html',
  styleUrls: ['./publication-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PublicationFormComponent {
  @Input('publication')
  set publication(input: PublicationFormInput | null | undefined) {
    if (input) {
      this.isUpdate = true;
      this.publicationFromGroup.patchValue({

      });
    }
  }

  @Output('onSubmit')
  onSubmit = new EventEmitter<PublicationFormOutput>();

  constructor(private formBuilder: FormBuilder) {}

  isUpdate: boolean = false;
  publicationFromGroup: FormGroup = this.formBuilder.group({
    title: ['', Validators.required],
      publicationType: ['', Validators.required],
      publishedDate: ['', Validators.required],
      publisher: ['', Validators.required],
      isbn: [''],
      edition: ['', Validators.required],
      language: ['', Validators.required],
      description: ['', Validators.required],
      authors: [[]],
      buyingPrice: ['', [Validators.required, Validators.min(0)]],
      callNumber: ['', Validators.required],
      copiesAvailable: ['', [Validators.required, Validators.min(0)]],
  });

  submitForm() {
    if (!this.publicationFromGroup.valid) {
      this.publicationFromGroup.markAllAsTouched();
      return;
    }

    this.onSubmit.emit(this.publicationFromGroup.value);
  }

  resetForm() {
    this.publicationFromGroup.reset();
  }
}
