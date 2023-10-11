import { EventEmitter } from "@angular/core";
import { FormGroup, FormControl } from "@angular/forms";

export abstract class BaseFormComponent<TOutput>{
  protected readonly formGroup: FormGroup<{
    [K in keyof TOutput]: FormControl<TOutput[K]>;
  }>;
  protected submitEventEmitter: EventEmitter<TOutput> = new EventEmitter<TOutput>;

  protected abstract createFormGroup(): FormGroupModel<TOutput>;

  constructor() {
    this.formGroup = this.createFormGroup();
  }

  submitForm(): void {
    if (!this.formGroup.valid) {
      this.formGroup.markAllAsTouched();
      return;
    }
    const formValue = this.formGroup.getRawValue() as TOutput;
    this.submitEventEmitter.emit(formValue);
  }

  resetForm(): void {
    this.formGroup.reset();
  }
}

export interface FormGroupModel<TOutput> extends FormGroup<{
  [K in keyof TOutput]: FormControl<TOutput[K]>;
}> { }
