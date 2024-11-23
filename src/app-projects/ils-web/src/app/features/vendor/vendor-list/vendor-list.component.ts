import {Component, OnInit} from '@angular/core';
import {BasePaginatedListComponent} from "../../../abstractions/base-paginated-list-component";
import {
    DeleteVendorGQL,
    GetVendorsGQL,
    GetVendorsQuery,
    GetVendorsQueryVariables,
    SortEnumType,
    VendorStatus
} from "@kathanika/graphql-client";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {CommonModule} from "@angular/common";
import {KnBadge, KnButton, KnChip, KnPagination} from "@kathanika/kn-ui";
import {MessageAlertService} from "../../../core/message-alert.service";

@Component({
    selector: 'app-vendor-list',
    standalone: true,
    imports: [
        CommonModule,
        RouterLink,
        KnButton,
        KnPagination,
        KnBadge
    ],
    templateUrl: './vendor-list.component.html'
})
export class VendorListComponent extends BasePaginatedListComponent<GetVendorsQuery, GetVendorsQueryVariables> implements OnInit {
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

    constructor(
        gql: GetVendorsGQL,
        private deleteVendorGql: DeleteVendorGQL,
        private alertService: MessageAlertService,
        activatedRoute: ActivatedRoute,
        router: Router
    ) {
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
