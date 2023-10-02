import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { PublicationFormInput } from '../../types/publication-form-input';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { PublicationFormOutput } from '../../types/publication-form-output';
import { PublicationType } from 'src/app/graphql/generated/graphql-operations';
import { BaseFormComponent, ControlsOf } from 'src/app/shared/bases/base-form-component';

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
      this.isUpdate = true;
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

  constructor(private formBuilder: FormBuilder) {
    super();
  }

  protected createFormGroup(): FormGroup<ControlsOf<PublicationFormOutput>> {
    return new FormGroup<ControlsOf<PublicationFormOutput>>({
      title: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      publicationType: new FormControl<PublicationType>(PublicationType.Book, { nonNullable: true, validators: [Validators.required] }),
      publishedDate: new FormControl<Date | null>(null, { nonNullable: true, validators: [Validators.required] }),
      publisher: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      isbn: new FormControl<string | null>(null, { nonNullable: false }),
      edition: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      language: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      description: new FormControl<string | null>(null, { nonNullable: false }),
      authorIds: new FormControl<string[]>([], { nonNullable: true }),
      buyingPrice: new FormControl<number>(0, { nonNullable: true, validators: [Validators.required, Validators.min(0)] }),
      callNumber: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      copiesPurchased: new FormControl<number>(0, { nonNullable: true, validators: [Validators.required, Validators.min(0)] }),
    })
  }

  isUpdate: boolean = false;
}
