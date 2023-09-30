import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { PublicationFormInput } from '../../types/publication-form-input';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { PublicationFormOutput } from '../../types/publication-form-output';
import { PublicationType } from 'src/app/graphql/generated/graphql-operations';

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
        ...input,
        authorsIds: input.authors,
      });
    }
  }

  @Output('onSubmit')
  onSubmit = new EventEmitter<PublicationFormOutput>();

  publicationTypes: string[] = Object.values(PublicationType);

  constructor(private formBuilder: FormBuilder) { }

  isUpdate: boolean = false;
  publicationFromGroup: FormGroup = this.formBuilder.group({
    title: [null, Validators.required],
    publicationType: [null, Validators.required],
    publishedDate: [null, Validators.required],
    publisher: [null, Validators.required],
    isbn: [null],
    edition: [null, Validators.required],
    language: [null, Validators.required],
    description: [null],
    authorIds: [[]],
    buyingPrice: [null, [Validators.required, Validators.min(0)]],
    callNumber: [null, Validators.required],
    copiesPurchased: [null, [Validators.required, Validators.min(0)]],
  });

  submitForm() {
    console.debug(this.publicationFromGroup.value);
    if (!this.publicationFromGroup.valid) {
      this.publicationFromGroup.markAllAsTouched();
      return;
    }
    this.onSubmit.emit({
      ...this.publicationFromGroup.value,
      buyingPrice: +this.publicationFromGroup.value.buyingPrice,
      copiesPurchased: +this.publicationFromGroup.value.copiesPurchased
    });
  }

  resetForm() {
    this.publicationFromGroup.reset();
  }
}
