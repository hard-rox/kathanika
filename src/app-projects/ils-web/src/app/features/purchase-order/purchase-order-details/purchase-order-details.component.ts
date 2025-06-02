import {Component, inject, OnInit} from '@angular/core';
import {BaseQueryComponent} from "../../../abstractions/base-query-component";
import {
    PurchaseOrderDetailsQuery,
    PurchaseOrderDetailsQueryVariables, PurchaseOrderDetailsGQL, PurchaseOrderStatus
} from "../../../graphql/generated/graphql-operations";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {CommonModule} from "@angular/common";
import {KnBadge, KnButton, KnPanel} from "@kathanika/kn-ui";

@Component({
    imports: [
        CommonModule,
        KnPanel,
        KnBadge,
        KnButton,
        RouterLink
    ],
    standalone: true,
    templateUrl: './purchase-order-details.component.html'
})
export class PurchaseOrderDetailsComponent
    extends BaseQueryComponent<PurchaseOrderDetailsQuery, PurchaseOrderDetailsQueryVariables>
    implements OnInit {
    private readonly activatedRoute = inject(ActivatedRoute);

    constructor() {
        const gql = inject(PurchaseOrderDetailsGQL);
        super(gql);
    }

    ngOnInit(): void {
        const purchaseOrderId = this.activatedRoute.snapshot.params['purchaseOrderId'];
        if (purchaseOrderId && purchaseOrderId.length > 0) {
            this.queryVariables = {
                id: purchaseOrderId,
            };
            this.queryRef.refetch(this.queryVariables);
        }
    }

    protected readonly purchaseOrderStatus = PurchaseOrderStatus;
}