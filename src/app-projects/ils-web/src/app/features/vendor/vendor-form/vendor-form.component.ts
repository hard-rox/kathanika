import {Component, Input, Output} from '@angular/core';
import {BaseFormComponent, FormControlsOf} from "../../../abstractions/base-form-component";
import {AddVendorInput, Vendor, VendorPatchInput} from "../../../graphql/generated/graphql-operations";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {KnButton, KnTextareaInput, KnTextInput} from "@kathanika/kn-ui";
import {CommonModule} from "@angular/common";

@Component({
    selector: 'app-vendor-form',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        KnTextInput,
        KnTextareaInput,
        KnButton
    ],
    standalone: true,
    templateUrl: './vendor-form.component.html'
})
export class VendorFormComponent extends BaseFormComponent<AddVendorInput | VendorPatchInput> {
    @Input()
    set vendor(input: Vendor | null) {
        if (input) {
            this.formGroup.patchValue(input);
        }
    }

    protected override createFormGroup(): FormGroup<FormControlsOf<AddVendorInput | VendorPatchInput>> {
        return new FormGroup<FormControlsOf<AddVendorInput | VendorPatchInput>>({
            name: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
            accountDetail: new FormControl<string | null>(null),
            address: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
            contactNumber: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
            contactPersonEmail: new FormControl<string | null>(null, [Validators.email]),
            contactPersonName: new FormControl<string | null>(null),
            contactPersonPhone: new FormControl<string | null>(null),
            email: new FormControl<string | null>(null, [Validators.email]),
            website: new FormControl<string | null>(null)
        });
    }
}
