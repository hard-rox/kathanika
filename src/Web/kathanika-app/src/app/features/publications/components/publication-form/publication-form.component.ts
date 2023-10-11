import { ChangeDetectionStrategy, Component, Input, Output } from '@angular/core';
import { PublicationFormInput } from '../../types/publication-form-input';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { PublicationFormOutput } from '../../types/publication-form-output';
import { PublicationType } from 'src/app/graphql/generated/graphql-operations';
import { BaseFormComponent, FormGroupModel } from 'src/app/shared/bases/base-form-component';
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

  protected createFormGroup(): FormGroupModel<PublicationFormOutput> | any{
    return new FormGroup({
      title: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      publicationType: new FormControl<PublicationType | null>(null, { nonNullable: true, validators: [Validators.required] }),
      publishedDate: new FormControl<Date | null>(null, { nonNullable: true, validators: [Validators.required] }),
      publisher: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      isbn: new FormControl<string | null>(null, { nonNullable: false }),
      edition: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      language: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      description: new FormControl<string | null>(null, { nonNullable: false }),
      authorIds: new FormControl<string[]>([], { nonNullable: true }),
      buyingPrice: new FormControl<number | null>(null, { nonNullable: true, validators: [Validators.required, Validators.min(0)] }),
      callNumber: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      copiesPurchased: new FormControl<number | null>(null, { nonNullable: true, validators: [Validators.required, Validators.min(1), KnValidators.integerOnly] }),
    });
  }
}
