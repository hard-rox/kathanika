import { Component, OnInit, inject } from '@angular/core';
import {BaseQueryComponent} from "../../../abstractions/base-query-component";
import {
    VendorDetailsGQL,
    VendorDetailsQuery,
    VendorDetailsQueryVariables
} from "../../../graphql/generated/graphql-operations";
import {ActivatedRoute} from "@angular/router";
import {CommonModule} from "@angular/common";
import {KnPanel} from "@kathanika/kn-ui";

@Component({
    selector: 'app-vendor-details',
        imports: [
        CommonModule,
        KnPanel
    ],
    templateUrl: './vendor-details.component.html'
})
export class VendorDetailsComponent extends BaseQueryComponent<VendorDetailsQuery, VendorDetailsQueryVariables>
    implements OnInit {
    private activatedRoute = inject(ActivatedRoute);

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
}