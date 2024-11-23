import { EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

export abstract class BaseFormComponent<TOutput extends Record<string, unknown>> {
    protected readonly formGroup: FormGroup<ControlsOf<TOutput>>;
    protected submitEventEmitter: EventEmitter<TOutput> =
        new EventEmitter<TOutput>();

    protected abstract createFormGroup(): FormGroup<ControlsOf<TOutput>>;

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

export type ControlsOf<T extends Record<string, unknown>> = {
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    [K in keyof T]: T[K] extends Record<any, unknown>
        ? FormGroup<ControlsOf<T[K]>>
        : FormControl<T[K]>;
};