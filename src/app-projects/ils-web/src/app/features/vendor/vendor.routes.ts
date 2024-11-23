import {Routes} from "@angular/router";
import {VendorListComponent} from "./vendor-list/vendor-list.component";
import {VendorDetailsComponent} from "./vendor-details/vendor-details.component";
import {VendorAddComponent} from "./vendor-add/vendor-add.component";
import {VendorUpdateComponent} from "./vendor-update/vendor-update.component";

export const vendorRoutes: Routes = [
    {
        path: '',
        component: VendorListComponent
    },
    {
        path: 'add',
        component: VendorAddComponent
    },
    {
        path: 'update/:vendorId',
        component: VendorUpdateComponent,
    },
    {
        path: ':vendorId',
        component: VendorDetailsComponent
    }
]