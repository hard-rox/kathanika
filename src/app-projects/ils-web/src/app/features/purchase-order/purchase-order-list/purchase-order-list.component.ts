import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from "@angular/common";
import {KnBadge, KnButton, KnPagination} from "@kathanika/kn-ui";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {BasePaginatedListComponent} from "../../../abstractions/base-paginated-list-component";
import {
    PurchaseOrderListGQL,
    PurchaseOrderListQuery,
    PurchaseOrderListQueryVariables,
    PurchaseOrderStatus
} from "../../../graphql/generated/graphql-operations";

@Component({
    imports: [
        CommonModule,
        KnBadge,
        KnButton,
        KnPagination,
        RouterLink
    ],
    templateUrl: './purchase-order-list.component.html'
})
export class PurchaseOrderListComponent 
    extends BasePaginatedListComponent<PurchaseOrderListQuery, PurchaseOrderListQueryVariables> 
    implements OnInit {
    protected override setSearchTextQueryFilter(searchText: string): void {
        if (!searchText || searchText.length == 0) {
            this.queryVariables.filter = null;
            return;
        }

        this.queryVariables.filter = {
            or: [
                {
                    vendorName: {
                        contains: searchText,
                    },
                }
            ],
        };
    }

    constructor() {
        const gql = inject(PurchaseOrderListGQL);
        const activatedRoute = inject(ActivatedRoute);
        const router = inject(Router);

        super(gql, activatedRoute, router, {
            skip: 0,
            take: 20,
            // TODO: Creation date 
            // sortBy: {
            //     : SortEnumType.Asc,
            // },
        });
    }

    protected purchaseOrderStatus = PurchaseOrderStatus;

    ngOnInit(): void {
        this.init();
    }

}