import {ComponentFixture, TestBed} from '@angular/core/testing';

import {PurchaseOrderDetailsComponent} from './purchase-order-details.component';
import {ActivatedRoute} from "@angular/router";
import {PurchaseOrderDetailsGQL} from "../../../graphql/generated/graphql-operations";
import {mockQueryGql} from "../../../graphql/gql-test-utils";

describe('PurchaseOrderDetailsComponent', () => {
    let component: PurchaseOrderDetailsComponent;
    let fixture: ComponentFixture<PurchaseOrderDetailsComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [PurchaseOrderDetailsComponent],
            providers: [
                {
                    provide: ActivatedRoute,
                    useValue: {snapshot: {params: {id: 112233}}}
                }, {
                    provide: PurchaseOrderDetailsGQL,
                    useValue: mockQueryGql
                }
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(PurchaseOrderDetailsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
