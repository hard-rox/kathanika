import {Component, Input, Output, ViewChild} from '@angular/core';
import {BaseFormComponent, ControlsOf} from "../../../abstractions/base-form-component";
import {
    CreatePurchaseOrderInput, PurchaseItemInput, PurchaseOrder
} from "../../../graphql/generated/graphql-operations";
import {FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {KnButton, KnModal, KnNumberInput, KnSearchbar, KnTextareaInput, KnTextInput} from "@kathanika/kn-ui";

@Component({
    selector: 'app-purchase-order-form',
    templateUrl: './purchase-order-form.component.html',
    imports: [
        CommonModule,
        KnTextareaInput,
        ReactiveFormsModule,
        KnSearchbar,
        KnTextInput,
        KnButton,
        KnNumberInput,
        KnModal
    ]
})
export class PurchaseOrderFormComponent extends BaseFormComponent<CreatePurchaseOrderInput> {
    @Input()
    set vendor(input: PurchaseOrder | null) {
        if (input) {
            this.formGroup.patchValue(input);
        }
    }

    @Output()
    formSubmit = this.submitEventEmitter;
    
    @ViewChild('purchaseItemFormModal')
    purchaseItemFormModal!: KnModal;
    
    protected get purchaseItemFormGroup() {
        return new FormGroup<ControlsOf<PurchaseItemInput>>({
            title: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
            quantity: new FormControl(0, {nonNullable: true, validators: [Validators.required]}),
            author: new FormControl<string | null>(null, {nonNullable: false}),
            edition: new FormControl<string | null>(null, {nonNullable: false}),
            internalNote: new FormControl<string | null>(null, {nonNullable: false}),
            isbn: new FormControl<string | null>(null, {nonNullable: false}),
            publisher: new FormControl<string | null>(null, {nonNullable: false}),
            publishingYear: new FormControl<number | null>(null, {nonNullable: false}),
            vendorNote: new FormControl<string | null>(null, {nonNullable: false}),
            vendorPrice: new FormControl<number | null>(null, {nonNullable: false}),
        });
    }

    protected override createFormGroup(): FormGroup<ControlsOf<CreatePurchaseOrderInput>> {
        return new FormGroup<ControlsOf<CreatePurchaseOrderInput>>({
            vendorId: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
            internalNote: new FormControl<string | null>(null, {nonNullable: false}),
            vendorNote: new FormControl<string | null>(null, {nonNullable: false}),
            items: new FormArray<FormGroup<ControlsOf<PurchaseItemInput>>>([])
        });
    }

    addItem() {
        this.purchaseItemFormModal.show();
    }

    removeItem(index: number) {
        this.formGroup.controls.items.removeAt(index);
    }

    protected readonly FormArray = FormArray;
}
