import { FormGroup, ValidationErrors } from "@angular/forms"
import { environment } from "src/environments/environment";

export function printFormGroupValidationErrors(formGroup: FormGroup): void {
  if (formGroup == null || formGroup == undefined || environment.production) return;
  Object.keys(formGroup.controls).forEach(key => {
    const controlErrors: ValidationErrors | null | undefined = formGroup.get(key)?.errors;
    if (controlErrors != null) {
      Object.keys(controlErrors).forEach(keyError => {
        console.log('Key control: ' + key + ', keyError: ' + keyError + ', err value: ', controlErrors[keyError]);
      });
    }
  });
}
