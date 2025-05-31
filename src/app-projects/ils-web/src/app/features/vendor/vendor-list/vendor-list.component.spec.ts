import {ComponentFixture, TestBed} from '@angular/core/testing';

import {VendorListComponent} from './vendor-list.component';
import {CommonModule} from "@angular/common";
import {ActivatedRoute, RouterLink} from "@angular/router";
import {KnBadge, KnButton, KnPagination} from "@kathanika/kn-ui";
import {DeleteVendorGQL, VendorDetailsGQL} from "../../../graphql/generated/graphql-operations";
import {of} from "rxjs";
import {mockMutationGql, mockQueryGql} from "../../../graphql/gql-test-utils";

describe('VendorListComponent', () => {
    let component: VendorListComponent;
    let fixture: ComponentFixture<VendorListComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                VendorListComponent,
                CommonModule,
                RouterLink,
                KnButton,
                KnPagination,
                KnBadge
            ],
            providers: [
                {
                    provide: VendorDetailsGQL,
                    useValue: mockQueryGql,
                },
                {
                    provide: DeleteVendorGQL,
                    useValue: mockMutationGql,
                },
                {
                    provide: ActivatedRoute,
                    useValue: {
                        queryParams: of({
                            size: 20,
                            page: 2,
                        }),
                    },
                },
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(VendorListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    xit('should create', () => {
        expect(component).toBeTruthy();
    });
});
