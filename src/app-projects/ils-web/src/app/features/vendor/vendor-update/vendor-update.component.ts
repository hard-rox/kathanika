import { Component, OnInit, inject, viewChild } from '@angular/core';
import {VendorFormComponent} from "../vendor-form/vendor-form.component";
import {AddVendorInput, GetVendorGQL, UpdateVendorGQL, Vendor, VendorPatchInput} from "@kathanika/graphql-client";
import {MessageAlertService} from "../../../core/message-alert.service";
import {ActivatedRoute, Router} from "@angular/router";
import {finalize} from "rxjs";
import {KnAlert, KnPanel} from "@kathanika/kn-ui";

@Component({
    selector: 'app-vendor-update',
        imports: [
        KnAlert,
        KnPanel,
        VendorFormComponent
    ],
    templateUrl: './vendor-update.component.html'
})
export class VendorUpdateComponent implements OnInit {
    private gql = inject(GetVendorGQL);
    private mutation = inject(UpdateVendorGQL);
    private alertService = inject(MessageAlertService);
    private activatedRoute = inject(ActivatedRoute);
    private router = inject(Router);

    readonly vendorUpdateForm = viewChild.required<VendorFormComponent>('vendorUpdateForm');

    isPanelLoading = true;
    vendorId: string | undefined;
    vendorToUpdate: Vendor | null = null;
    errors: string[] = [];

    ngOnInit(): void {
        this.vendorId = this.activatedRoute.snapshot.params['vendorId'];
        if (this.vendorId && this.vendorId.length > 0) {
            this.gql
                .fetch({
                    id: this.vendorId,
                })
                .pipe(finalize(() => {
                    this.isPanelLoading = false;
                }))
                .subscribe({
                    next: (result) => {
                        if (result.error || result.errors) {
                            this.alertService.showPopup(
                                'error',
                                result.error?.message ??
                                (result.errors?.join('<br/>') as string),
                            );
                        } else if (result.data.vendor == null) {
                            this.alertService.showPopup(
                                'error',
                                'Returning to list page.',
                                'Vendor not found',
                            );
                            this.router.navigate(['/vendors']);
                        } else {
                            this.vendorToUpdate = {...result.data.vendor};
                        }
                    },
                    error: (err) => {
                        this.alertService.showHttpErrorPopup(err);
                    },
                });
        }
    }

    onValidFormSubmit(vendorOutput: AddVendorInput | VendorPatchInput) {
        this.isPanelLoading = true;

        this.mutation
            .mutate({
                id: this.vendorId as string,
                patch: vendorOutput,
            })
            .pipe(finalize(() => {
                this.isPanelLoading = false;
            }))
            .subscribe({
                next: (result) => {
                    // console.debug(JSON.stringify(result));
                    if (result.errors || result.data?.updateVendor.errors) {
                        this.errors = [];
                        result.data?.updateVendor.errors?.forEach((x) => {
                            switch (x.__typename) {
                                case 'ValidationError':
                                    this.errors.push(`${x.fieldName} - ${x.message}`);
                                    break;
                                default:
                                    this.errors.push(x.message);
                                    break;
                            }
                        });
                        result.errors?.forEach((x) => this.errors.push(x.message));
                    } else {
                        this.alertService.showToast(
                            'success',
                            result.data?.updateVendor.message ?? 'Vendor updated.',
                        );
                        this.vendorUpdateForm()?.resetForm();
                        this.router.navigate([
                            `/vendors/${result.data?.updateVendor.data?.id}`,
                        ]);
                    }
                },
                error: (err) => {
                    this.alertService.showHttpErrorPopup(err)
                },
            });
    }

    closeAlert() {
        this.errors = [];
    }
}
