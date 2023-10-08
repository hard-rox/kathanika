import { ChangeDetectionStrategy, Component, Input, Output } from '@angular/core';
import { PublicationFormInput } from '../../types/publication-form-input';
import { FormControl, Validators } from '@angular/forms';
import { PublicationFormOutput } from '../../types/publication-form-output';
import { PublicationType } from 'src/app/graphql/generated/graphql-operations';
import { BaseFormComponent } from 'src/app/shared/bases/base-form-component';
import { KnValidators } from 'src/app/shared/validators/kn-validators';

@Component({
  selector: 'kn-publication-form',
  templateUrl: './publication-form.component.html',
  styleUrls: ['./publication-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PublicationFormComponent
  extends BaseFormComponent<PublicationFormOutput> {

  @Input('publication')
  set publication(input: PublicationFormInput | null | undefined) {
    if (input) {
      this.formGroup.patchValue({
        ...input,
        authorIds: input.authors,
        copiesPurchased: input.copiesAvailable
      });
    }
  }

  @Output('onSubmit')
  onSubmit = this.submitEventEmitter;

  publicationTypes: string[] = Object.values(PublicationType);

  constructor() {
    super();
    this.addControlsToForm();
  }

  private addControlsToForm() {
    this.formGroup.addControl('title', new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }));
    this.formGroup.addControl('publicationType', new FormControl<PublicationType | null>(null, { nonNullable: true, validators: [Validators.required] }));
    this.formGroup.addControl('publishedDate', new FormControl<Date | null>(null, { nonNullable: true, validators: [Validators.required] }));
    this.formGroup.addControl('publisher', new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }));
    this.formGroup.addControl('isbn', new FormControl<string | null>(null, { nonNullable: false }));
    this.formGroup.addControl('edition', new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }));
    this.formGroup.addControl('language', new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }));
    this.formGroup.addControl('description', new FormControl<string | null>(null, { nonNullable: false }));
    this.formGroup.addControl('authorIds', new FormControl<string[]>([], { nonNullable: true }));
    this.formGroup.addControl('buyingPrice', new FormControl<number | null>(null, { nonNullable: true, validators: [Validators.required, Validators.min(0)] }));
    this.formGroup.addControl('callNumber', new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }));
    this.formGroup.addControl('copiesPurchased', new FormControl<number | null>(null, { nonNullable: true, validators: [Validators.required, Validators.min(1), KnValidators.integerOnly] }));
  }
}
