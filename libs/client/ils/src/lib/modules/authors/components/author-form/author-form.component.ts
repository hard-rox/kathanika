import { Component, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthorFormInput } from '../../types/author-form-input';
import { AuthorFormOutput } from '../../types/author-form-output';
import { BaseFormComponent, FormGroupModel } from '../../../../abstractions/base-form-component';
@Component({
  selector: 'kn-author-form',
  templateUrl: './author-form.component.html',
  styleUrls: ['./author-form.component.scss'],
})
export class AuthorFormComponent
  extends BaseFormComponent<AuthorFormOutput>
  implements OnInit {

  @Input()
  set author(input: AuthorFormInput | null | undefined) {
    if (input) {
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

  @Output()
  formSubmit = this.submitEventEmitter;

  protected createFormGroup(): FormGroupModel<AuthorFormOutput> {
    const group: FormGroupModel<AuthorFormOutput> = new FormGroup({
      firstName: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      lastName: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      dateOfBirth: new FormControl<Date | null>(null, { nonNullable: true, validators: [Validators.required] }),
      markedAsDeceased: new FormControl<boolean>(false, { nonNullable: true }),
      dateOfDeath: new FormControl<Date | null>(null, { nonNullable: false }),
      nationality: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      biography: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
    });
    return group;
  }

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
