import {Component, OnInit, inject} from '@angular/core';
import {BasePaginatedListComponent} from "../../../abstractions/base-paginated-list-component";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {CommonModule} from "@angular/common";
import {KnBadge, KnButton, KnPagination} from "@kathanika/kn-ui";
import {MessageAlertService} from "../../../core/message-alert/message-alert.service";
import {
    DeleteVendorGQL,
    SortEnumType,
    VendorListGQL,
    VendorListQuery,
    VendorListQueryVariables, VendorStatus
} from "../../../graphql/generated/graphql-operations";

@Component({
    selector: 'app-vendor-list',
    imports: [
        CommonModule,
        RouterLink,
        KnButton,
        KnPagination,
        KnBadge
    ],
    standalone: true,
    templateUrl: './vendor-list.component.html'
})
export class VendorListComponent extends BasePaginatedListComponent<VendorListQuery, VendorListQueryVariables> implements OnInit {
    private deleteVendorGql = inject(DeleteVendorGQL);
    private alertService = inject(MessageAlertService);

    protected setSearchTextQueryFilter(searchText: string): void {
        if (!searchText || searchText.length == 0) {
            this.queryVariables.filter = null;
            return;
        }

        this.queryVariables.filter = {
            or: [
                {
                    name: {
                        contains: searchText,
                    },
                }
            ],
        };
    }

    constructor() {
        const gql = inject(VendorListGQL);
        const activatedRoute = inject(ActivatedRoute);
        const router = inject(Router);

        super(gql, activatedRoute, router, {
            skip: 0,
            take: 20,
            sortBy: {
                name: SortEnumType.Asc,
            },
        });
    }

    vendorStatus = VendorStatus;

    ngOnInit(): void {
        this.init();
    }

    deleteVendor(vendorId: string) {
        this.alertService.showConfirmation('warning', 'Are you sure you want to delete Vendor?')
            .subscribe({
                next: (confirmed) => {
                    if (confirmed) {
                        this.deleteVendorGql.mutate({
                            id: vendorId
                        }).subscribe({
                            next: (result) => {
                                if (result.errors) {
                                    this.alertService.showPopup(
                                        'error',
                                        (result.errors?.join('<br/>') as string),
                                    );
                                    return;
                                }
                                this.alertService.showPopup('success', result.data?.deleteVendor.message ?? 'Vendor deleted', 'Deleted');
                            },
                            error: (err) => {
                                this.alertService.showHttpErrorPopup(err);
                            },
                        })
                    }
                }
            })
    }
}
