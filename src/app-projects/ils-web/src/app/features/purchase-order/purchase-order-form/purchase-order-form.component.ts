import {Component, computed, inject, Input, signal} from '@angular/core';
import {BaseFormComponent, FormControlsOf} from "../../../abstractions/base-form-component";
import {
    CreatePurchaseOrderInput, PurchaseItemInput,
    PurchaseOrder,
    SearchVendorsGQL,
    SearchVendorsQuery,
    SearchVendorsQueryVariables
} from "../../../graphql/generated/graphql-operations";
import {FormArray, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {CommonModule} from "@angular/common";
import {
    KnButton,
    KnSearchbar,
    KnTextareaInput
} from "@kathanika/kn-ui";
import {ModalDialogService} from "../../../core/modal-dialog/modal-dialog.service";
import {PurchaseItemFormComponent} from "../purchase-item-form/purchase-item-form.component";
import {QueryRef} from "apollo-angular";

@Component({
    selector: 'app-purchase-order-form',
    templateUrl: './purchase-order-form.component.html',
    standalone: true,
    imports: [
        CommonModule,
        KnTextareaInput,
        ReactiveFormsModule,
        KnSearchbar,
        KnButton
    ]
})
export class PurchaseOrderFormComponent extends BaseFormComponent<CreatePurchaseOrderInput> {
    private modalDialogService = inject(ModalDialogService);
    @Input()
    set vendor(input: PurchaseOrder | null) {
        if (input) {
            this.formGroup.patchValue(input);
        }
    }

    protected readonly purchaseItems = signal<PurchaseItemInput[]>([]);
    protected readonly totalCost = computed(() => {
        return this.purchaseItems().reduce((total, item) => {
            const price = item.vendorPrice ?? 0;
            return total + (item.quantity * price);
        }, 0);
    });
    protected readonly vendorSearchQueryRef: QueryRef<SearchVendorsQuery, SearchVendorsQueryVariables>;

    constructor() {
        const vendorSearchGql = inject(SearchVendorsGQL);

        super();
        this.vendorSearchQueryRef = vendorSearchGql.watch({searchTerm: ''});
    }

    createItem(data?: Partial<PurchaseItemInput>): FormGroup {
        return new FormGroup({
            title: new FormControl(data?.title ?? '', {validators: [Validators.required]}),
            quantity: new FormControl(data?.quantity ?? 1, {validators: [Validators.required, Validators.min(1)]}),
            author: new FormControl(data?.author ?? ''),
            publisher: new FormControl(data?.publisher ?? ''),
            edition: new FormControl(data?.edition ?? ''),
            publishingYear: new FormControl(data?.publishingYear ?? null),
            isbn: new FormControl(data?.isbn ?? ''),
            vendorPrice: new FormControl(data?.vendorPrice ?? null),
            internalNote: new FormControl(data?.internalNote ?? ''),
            vendorNote: new FormControl(data?.vendorNote ?? ''),
        });
    }

    protected override createFormGroup(): FormGroup<FormControlsOf<CreatePurchaseOrderInput>> {
        return new FormGroup<FormControlsOf<CreatePurchaseOrderInput>>({
            vendorId: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
            internalNote: new FormControl<string | null>(null),
            vendorNote: new FormControl<string | null>(null),
            items: new FormArray<FormGroup<FormControlsOf<PurchaseItemInput>>>([], [Validators.required])
        });
    }

    getVendorName(vendor: { id: string, name: string }) {
        return vendor.name;
    }

    searchTextChanged(searchText: string) {
        console.debug(searchText);
        this.vendorSearchQueryRef.refetch({searchTerm: searchText});
    }

    addItem() {
        this.modalDialogService.open(PurchaseItemFormComponent)
            .then(result => {
                const item = result.data as PurchaseItemInput;
                if (!item)
                    return;
                this.purchaseItems
                    .update((prev) => [...prev, item]);
                this.formGroup.controls.items.push(this.createItem(item));
            });
    }

    removeItem(index: number) {
        this.purchaseItems
            .update((prev) => prev.filter((_, i) => i !== index));
        this.formGroup.patchValue({items: this.purchaseItems()})
    }

    updateItem(index: number) {
        console.debug(index);
        this.formGroup.patchValue({items: this.purchaseItems()})
    }

    selectVendor(vendor: { id: string, name: string }) {
        this.formGroup.patchValue({vendorId: vendor.id});
    }
}