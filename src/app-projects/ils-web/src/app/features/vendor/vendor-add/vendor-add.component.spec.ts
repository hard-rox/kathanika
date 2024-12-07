import {ComponentFixture, TestBed} from '@angular/core/testing';

import {VendorAddComponent} from './vendor-add.component';
import {VendorFormComponent} from "../vendor-form/vendor-form.component";
import {KnAlert, KnPanel} from "@kathanika/kn-ui";
import {VendorAddGQL} from "../../../graphql/generated/graphql-operations";
import {mockMutationGql} from "../../../graphql/gql-test-utils";

describe('VendorAddComponent', () => {
    let component: VendorAddComponent;
    let fixture: ComponentFixture<VendorAddComponent>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                VendorAddComponent,
                VendorFormComponent,
                KnAlert,
                KnPanel
            ],
            providers: [
                {
                    provide: VendorAddGQL,
                    useValue: mockMutationGql
                }
            ]
        })
            .compileComponents();

        fixture = TestBed.createComponent(VendorAddComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
