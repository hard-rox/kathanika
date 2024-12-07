import {ComponentFixture, TestBed} from '@angular/core/testing';

import {VendorDetailsComponent} from './vendor-details.component';
import {KnPanel} from "@kathanika/kn-ui";
import {ActivatedRoute} from "@angular/router";
import {mockQueryGql} from "../../../graphql/gql-test-utils";
import {VendorDetailsGQL} from "../../../graphql/generated/graphql-operations";

describe('VendorDetailsComponent', () => {
    let component: VendorDetailsComponent;
    let fixture: ComponentFixture<VendorDetailsComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [VendorDetailsComponent, KnPanel],
            providers: [
                {
                    provide: VendorDetailsGQL,
                    useValue: mockQueryGql
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
        }).compileComponents();

        fixture = TestBed.createComponent(VendorDetailsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
