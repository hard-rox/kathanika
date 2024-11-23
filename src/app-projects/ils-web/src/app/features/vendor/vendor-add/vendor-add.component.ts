import {Component, ViewChild} from '@angular/core';
import {VendorFormComponent} from "../vendor-form/vendor-form.component";
import {KnAlert, KnPanel} from "@kathanika/kn-ui";
import {AddVendorGQL, AddVendorInput, VendorPatchInput} from "@kathanika/graphql-client";
import {CommonModule} from "@angular/common";
import {MessageAlertService} from "../../../core/message-alert.service";
import {Router} from "@angular/router";
import {finalize} from "rxjs";

@Component({
    selector: 'app-vendor-add',
    standalone: true,
    imports: [
        CommonModule,
        VendorFormComponent,
        KnAlert,
        KnPanel
    ],
    templateUrl: './vendor-add.component.html'
})
export class VendorAddComponent {
    @ViewChild('addVendorForm') memberCreateForm: VendorFormComponent | undefined;

    constructor(
        private gql: AddVendorGQL,
        private alertService: MessageAlertService,
        private router: Router,
    ) {
    }

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
                    console.debug(result);
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
                            result.data?.addVendor.message ?? 'Member added.',
                        );
                        this.memberCreateForm?.resetForm();
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
