import {Component, inject, viewChild} from '@angular/core';
import {VendorFormComponent} from "../vendor-form/vendor-form.component";
import {KnAlert, KnPanel} from "@kathanika/kn-ui";
import {AddVendorGQL, AddVendorInput, VendorPatchInput} from "../../../graphql/generated/graphql-operations";
import {CommonModule} from "@angular/common";
import {MessageAlertService} from "../../../core/message-alert/message-alert.service";
import {Router} from "@angular/router";
import {finalize} from "rxjs";

@Component({
    selector: 'app-vendor-add',
    imports: [
        CommonModule,
        VendorFormComponent,
        KnAlert,
        KnPanel
    ],
    standalone: true,
    templateUrl: './vendor-add.component.html'
})
export class VendorAddComponent {
    private gql = inject(AddVendorGQL);
    private alertService = inject(MessageAlertService);
    private router = inject(Router);

    readonly memberCreateForm = viewChild<VendorFormComponent>('addVendorForm');

    isPanelLoading = false;
    errors: string[] = [];

    onValidFormSubmit(formValue: AddVendorInput | VendorPatchInput) {
        this.isPanelLoading = true;
        this.gql.mutate({input: formValue as AddVendorInput})
            .pipe(finalize(() => {
                this.isPanelLoading = false;
            }))
            .subscribe({
                next: (result) => {
                    if (result.loading) {
                        this.isPanelLoading = true;
                        return;
                    }

                    if (result.errors || result.data?.addVendor.errors) {
                        this.errors = [];
                        result.data?.addVendor.errors?.forEach((x) => {
                                switch (x?.__typename) {
                                    case 'ValidationError':
                                        this.errors.push(`${x.fieldName} - ${x.message}`);
                                        break;
                                    default:
                                        this.errors.push(x.message);
                                        break;
                                }
                            }
                        );
                        result.errors?.forEach((x) => this.errors.push(x.message));
                    } else {
                        this.alertService.showToast(
                            'success',
                            result.data?.addVendor.message ?? 'Vendor added.',
                        );
                        this.memberCreateForm()?.resetForm();
                        this.router.navigate([`/vendors/${result.data?.addVendor.data?.id}`]);
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
