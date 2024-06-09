import { Component, Input, Output } from '@angular/core';
import { BaseFormComponent, ControlsOf } from '../../../../abstractions/base-form-component';
import { Publication, PublicationPatchInput, PublicationType } from '@kathanika/graphql-ts-client';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'kn-publication-patch-form',
  templateUrl: './publication-patch-form.component.html',
})
export class PublicationPatchFormComponent extends BaseFormComponent<PublicationPatchInput> {

  @Input()
  set publication(input: Publication) {
    if (input) {
      this.publicationToUpdate = input;
      this.formGroup.patchValue({
        ...input,
        publisherId: input.publisher?.id,
        authorIds: input.authors.map((x) => x.id)
      });
    }
  }

  @Output()
  formSubmit = this.submitEventEmitter;

  protected publicationToUpdate: Publication | null = null;
  protected publicationTypes: string[] = Object.values(PublicationType);

  protected override createFormGroup(): FormGroup<ControlsOf<PublicationPatchInput>> {
    return new FormGroup<ControlsOf<PublicationPatchInput>>({
      title: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      publicationType: new FormControl<PublicationType | any>(null, {
        nonNullable: true,
        validators: [Validators.required],
      }),
      publishedDate: new FormControl<Date | null>(null, {
        nonNullable: true,
        validators: [Validators.required],
      }),
      publisherId: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      isbn: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      edition: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      description: new FormControl<string | null>(null, { nonNullable: false }),
      authorIds: new FormControl<string[]>([], { nonNullable: true }),
      callNumber: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      language: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      })
    })
  }
}
