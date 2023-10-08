import { EventEmitter } from "@angular/core";
import { FormGroup, ɵElement } from "@angular/forms";

export abstract class BaseFormComponent<TOutput>{
  protected formGroup: FormGroup<{
    [K in keyof TOutput]: ɵElement<TOutput[K], null>;
  }>;
  protected submitEventEmitter: EventEmitter<TOutput> = new EventEmitter<TOutput>;

  constructor() {
    this.formGroup = new FormGroup({}) as any;
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
