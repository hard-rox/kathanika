import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export class KnValidators {
  static integerOnly(control: AbstractControl): ValidationErrors | null {
    return integerOnlyValidator(control);
  }
}

function integerOnlyValidator(control: AbstractControl): ValidationErrors | null {
  const isValid: boolean = /^-?\d+$/.test(control.value);
  return !isValid ? { integerOnly: { value: control.value } } : null;
};
