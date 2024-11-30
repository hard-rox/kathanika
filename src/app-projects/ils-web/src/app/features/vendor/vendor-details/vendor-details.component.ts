import {Component, OnInit} from '@angular/core';
import {BaseQueryComponent} from "../../../abstractions/base-query-component";
import {GetVendorGQL, GetVendorQuery, GetVendorQueryVariables} from "@kathanika/graphql-client";
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
export class VendorDetailsComponent extends BaseQueryComponent<GetVendorQuery, GetVendorQueryVariables>
    implements OnInit {
    constructor(
        gql: GetVendorGQL,
        private activatedRoute: ActivatedRoute,
    ) {
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