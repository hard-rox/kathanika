import {Component, output, OutputEmitterRef} from '@angular/core';
import {FormGroup, FormControl, FormArray} from '@angular/forms';
import {InputMaybe, Scalars} from "../graphql/generated/graphql-operations";

@Component({
    template: ''
})
export abstract class BaseFormComponent<TOutput extends Record<string, unknown>> {
    protected readonly formGroup: FormGroup<FormControlsOf<TOutput>>;
    
    formSubmitted: OutputEmitterRef<TOutput> = output<TOutput>();

    protected abstract createFormGroup(): FormGroup<FormControlsOf<TOutput>>;

    constructor() {
        this.formGroup = this.createFormGroup();
    }

    submitForm(): void {
        if (!this.formGroup.valid) {
            this.formGroup.markAllAsTouched();
            return;
        }
        const formValue = this.formGroup.value as TOutput;
        this.formSubmitted.emit(formValue);
    }

    resetForm(): void {
        this.formGroup.reset();
    }
}


// **Reusable Mapping Object** for GraphQL Scalars → Angular FormControl
type ScalarToFormControlMap = {
    [K in keyof Scalars]: FormControl<Scalars[K]['input']>;
};

// **Generic Utility to Map GraphQL Scalars to FormControl**
type MapScalarToFormControl<T> = T extends keyof ScalarToFormControlMap
    ? ScalarToFormControlMap[T]
    : never;

// **Handles Nullable Scalars (InputMaybe<T>)**
type MapNullableScalarToFormControl<T> = T extends InputMaybe<infer U>
    ? MapScalarToFormControl<U> | FormControl<U | null>
    : MapScalarToFormControl<T>;

// **Utility Type: Converts GraphQL Input Types into Angular FormControls**
export type FormControlsOf<T> = {
    [K in keyof T]: T[K] extends (infer U)[]
        ? FormArray<FormGroup<FormControlsOf<U>>> // Convert arrays to FormArray<FormGroup<T>>
        : MapNullableScalarToFormControl<T[K]>; // Use mapped FormControl type
};