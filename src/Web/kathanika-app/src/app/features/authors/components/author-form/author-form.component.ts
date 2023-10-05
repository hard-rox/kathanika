import { Component, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthorFormInput } from '../../types/author-form-input';
import { AuthorFormOutput } from '../../types/author-form-output';
import { BaseFormComponent, ControlsOf } from 'src/app/shared/bases/base-form-component';
@Component({
  selector: 'kn-author-form',
  templateUrl: './author-form.component.html',
  styleUrls: ['./author-form.component.scss'],
})
export class AuthorFormComponent
  extends BaseFormComponent<AuthorFormOutput>
  implements OnInit {

  @Input('author')
  set author(input: AuthorFormInput | null | undefined) {
    if (input) {
      this.isUpdate = true;
      this.formGroup.patchValue({
        firstName: input.firstName,
        lastName: input.lastName,
        dateOfBirth: input.dateOfBirth,
        markedAsDeceased: input.dateOfDeath != null && input.dateOfDeath != undefined,
        dateOfDeath: input.dateOfDeath,
        nationality: input.nationality,
        biography: input.biography,
      });
    }
  }

  @Output('onSubmit')
  onSubmit = this.submitEventEmitter;

  protected createFormGroup(): FormGroup<ControlsOf<AuthorFormOutput>> {
    return new FormGroup<ControlsOf<AuthorFormOutput>>({
      firstName: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
      lastName: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
      dateOfBirth: new FormControl<Date | null>(null, {nonNullable: true, validators: [Validators.required]}),
      markedAsDeceased: new FormControl<boolean>(false, { nonNullable: true }),
      dateOfDeath: new FormControl<Date | null>(null, {nonNullable: false}),
      nationality: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
      biography: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
    });
  }

  isUpdate: boolean = false;

  ngOnInit(): void {
    this.formGroup.valueChanges.subscribe({
      next: (value) => {
        if (value.markedAsDeceased) {
          this.formGroup.controls.dateOfDeath.addValidators([Validators.required]);
        } else {
          this.formGroup.controls.dateOfDeath.removeValidators([Validators.required]);
        }
      }
    })
  }
}
