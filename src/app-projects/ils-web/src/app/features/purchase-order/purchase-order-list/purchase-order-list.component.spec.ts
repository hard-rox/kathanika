import {ComponentFixture, TestBed} from '@angular/core/testing';

import {PurchaseOrderListComponent} from './purchase-order-list.component';
import {ActivatedRoute} from "@angular/router";
import {PurchaseOrderListGQL} from "../../../graphql/generated/graphql-operations";
import {mockQueryGql} from "../../../graphql/gql-test-utils";
import {of} from "rxjs";

describe('PurchaseOrderListComponent', () => {
    let component: PurchaseOrderListComponent;
    let fixture: ComponentFixture<PurchaseOrderListComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [PurchaseOrderListComponent],
            providers: [
                {
                    provide: ActivatedRoute,
                    useValue: {
                        queryParams: of({
                            size: 20,
                            page: 2,
                        }),
                    },
                },
                {
                    provide: PurchaseOrderListGQL,
                    useValue: mockQueryGql
                }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(PurchaseOrderListComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
