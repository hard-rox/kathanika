import { Component, OnInit, inject } from '@angular/core';
import {BaseQueryComponent} from "../../../abstractions/base-query-component";
import {
    PurchaseOrderStatus,
    VendorDetailsGQL,
    VendorDetailsQuery,
    VendorDetailsQueryVariables, VendorStatus
} from "../../../graphql/generated/graphql-operations";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {CommonModule} from "@angular/common";
import {KnBadge, KnButton, KnPanel} from "@kathanika/kn-ui";
import {da} from "@faker-js/faker";

@Component({
    selector: 'app-vendor-details',
    imports: [
        CommonModule,
        KnPanel,
        KnBadge,
        KnButton,
        RouterLink
    ],
    templateUrl: './vendor-details.component.html'
})
export class VendorDetailsComponent extends BaseQueryComponent<VendorDetailsQuery, VendorDetailsQueryVariables>
    implements OnInit {
    private activatedRoute = inject(ActivatedRoute);
    protected purchaseOrderStatus = PurchaseOrderStatus;

    constructor() {
        const gql = inject(VendorDetailsGQL);
        super(gql);
    }

    ngOnInit(): void {
        const vendorId = this.activatedRoute.snapshot.params['vendorId'];
        if (vendorId && vendorId.length > 0) {
            this.queryVariables = {
                id: vendorId,
            };
            this.queryRef.refetch(this.queryVariables);
        }
    }

    protected readonly da = da;
    protected readonly vendorStatus = VendorStatus;
}