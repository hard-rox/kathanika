import {Component} from '@angular/core';
import {ModalDialogService} from "../../../core/modal-dialog/modal-dialog.service";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {FormControlsOf} from "../../../abstractions/base-form-component";
import {PurchaseItemInput} from "../../../graphql/generated/graphql-operations";
import {CommonModule} from "@angular/common";
import {KnButton, KnNumberInput, KnPanel, KnTextareaInput, KnTextInput} from "@kathanika/kn-ui";

@Component({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        KnTextInput,
        KnNumberInput,
        KnTextareaInput,
        KnPanel,
        KnButton
    ],
    templateUrl: './purchase-item-form.component.html'
})
export class PurchaseItemFormComponent {
    constructor(private readonly modalDialogService: ModalDialogService) {
    }

    protected purchaseItemFormGroup = new FormGroup<FormControlsOf<PurchaseItemInput>>({
        title: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
        quantity: new FormControl<number>(0, {nonNullable: true, validators: [Validators.required, Validators.min(1)]}),
        vendorPrice: new FormControl<number | null>(null, {nonNullable: false}),
        author: new FormControl<string | null>(null, {nonNullable: false}),
        edition: new FormControl<string | null>(null, {nonNullable: false}),
        internalNote: new FormControl<string | null>(null, {nonNullable: false}),
        isbn: new FormControl<string | null>(null, {nonNullable: false}),
        publisher: new FormControl<string | null>(null, {nonNullable: false}),
        publishingYear: new FormControl<number | null>(null, {nonNullable: false}),
        vendorNote: new FormControl<string | null>(null, {nonNullable: false})
    });

    submit() {
        if (!this.purchaseItemFormGroup.valid) {
            this.purchaseItemFormGroup.markAllAsTouched();
            return;
        }

        this.modalDialogService.close(this.purchaseItemFormGroup.value);
    }
}
