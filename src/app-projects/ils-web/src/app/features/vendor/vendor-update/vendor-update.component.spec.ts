import {ComponentFixture, TestBed} from '@angular/core/testing';

import {VendorUpdateComponent} from './vendor-update.component';
import {ActivatedRoute, RouterLink} from "@angular/router";
import {KnBadge, KnButton, KnPagination} from "@kathanika/kn-ui";
import {GetVendorGQL, mockMutationGql, mockQueryGql, UpdateVendorGQL} from "@kathanika/graphql-client";

describe('VendorUpdateComponent', () => {
    let component: VendorUpdateComponent;
    let fixture: ComponentFixture<VendorUpdateComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                VendorUpdateComponent,
                RouterLink,
                KnButton,
                KnPagination,
                KnBadge
            ],
            providers: [
                {
                    provide: GetVendorGQL,
                    useValue: mockQueryGql,
                },
                {
                    provide: UpdateVendorGQL,
                    useValue: mockMutationGql,
                },
                {
                    provide: ActivatedRoute,
                    useValue: {
                        snapshot: {
                            params: {
                                'vendorId': '11111223445688'
                            }
                        }
                    }
                }
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(VendorUpdateComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});