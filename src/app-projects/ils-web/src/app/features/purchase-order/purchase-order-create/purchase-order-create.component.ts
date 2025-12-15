import {Component, inject} from '@angular/core';
import {
    CreatePurchaseOrderGQL, CreatePurchaseOrderInput, PurchaseOrderPatchInput
} from "../../../graphql/generated/graphql-operations";
import {MessageAlertService} from "../../../core/message-alert/message-alert.service";
import {Router} from "@angular/router";
import {finalize} from "rxjs";
import {PurchaseOrderFormComponent} from "../purchase-order-form/purchase-order-form.component";
import {KnAlert, KnPanel} from "@kathanika/kn-ui";
import {CommonModule} from "@angular/common";

@Component({
    imports: [
        CommonModule,
        KnAlert,
        KnPanel,
        PurchaseOrderFormComponent
    ],
    templateUrl: './purchase-order-create.component.html',
    standalone: true
})
export class PurchaseOrderCreateComponent {
    private readonly gql = inject(CreatePurchaseOrderGQL);
    private readonly alertService = inject(MessageAlertService);
    private readonly router = inject(Router);
    isPanelLoading = false;
    errors: string[] = [];

    onValidFormSubmit(formValue: CreatePurchaseOrderInput | PurchaseOrderPatchInput) {
        this.isPanelLoading = true;
        this.gql.mutate({variables: {input: formValue as CreatePurchaseOrderInput}})
            .pipe(finalize(() => {
                this.isPanelLoading = false;
            }))
            .subscribe({
                next: (result) => {
                    if (result.loading) {
                        this.isPanelLoading = true;
                        return;
                    }

                    if (result.error || result.data?.createPurchaseOrder.errors) {
                        this.errors = [];
                        result.data?.createPurchaseOrder.errors?.forEach((x) => {
                                if (x?.__typename === 'ValidationError') {
                                    this.errors.push(`${x.fieldName} - ${x.message}`);
                                } else {
                                    this.errors.push(x.message);
                                }
                            }
                        );
                        if (result.error) {
                            this.errors.push(result.error.message);
                        }
                    } else {
                        this.alertService.showToast(
                            'success',
                            result.data?.createPurchaseOrder.message ?? 'PurchaseOrder created.',
                        );
                        // this.purchaseOrderEntryForm()?.resetForm();
                        this.router.navigate([`/purchase-orders/${result.data?.createPurchaseOrder.data?.id}`]).then();
                    }
                },
                error: (err) => {
                    this.alertService.showHttpErrorPopup(err);
                }
            });
    }

    closeAlert() {
        this.errors = [];
    }
}
