import { Component, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  BaseFormComponent,
  ControlsOf
} from '../../../../abstractions/base-form-component';
import { AddAuthorInput, Author, AuthorPatchInput } from '@kathanika/graphql-ts-client';
@Component({
  selector: 'kn-author-form',
  templateUrl: './author-form.component.html'
})
export class AuthorFormComponent
  extends BaseFormComponent<AddAuthorInput | AuthorPatchInput> {
  @Input()
  set author(input: Author) {
    if (input) {
      this.formGroup.patchValue({
        firstName: input.firstName,
        lastName: input.lastName,
        dateOfBirth: input.dateOfBirth,
        markedAsDeceased:
          input.dateOfDeath != null && input.dateOfDeath != undefined,
        dateOfDeath: input.dateOfDeath,
        nationality: input.nationality,
        biography: input.biography,
      });
    }
  }

  @Output()
  formSubmit = this.submitEventEmitter;

  protected override createFormGroup(): FormGroup<ControlsOf<AddAuthorInput | AuthorPatchInput>> {
    return new FormGroup<ControlsOf<AddAuthorInput | AuthorPatchInput>>({
      firstName: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      lastName: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      dateOfBirth: new FormControl<Date | null>(null, {
        nonNullable: true,
        validators: [Validators.required],
      }),
      dateOfDeath: new FormControl<Date | null>(null),
      markedAsDeceased: new FormControl<boolean>(false, { nonNullable: true }),
      nationality: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      biography: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      dpFileId: new FormControl<string | null>(null)
    });
  }
}
