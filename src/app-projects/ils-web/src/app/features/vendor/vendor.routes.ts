import {Routes} from "@angular/router";
import {VendorListComponent} from "./vendor-list/vendor-list.component";
import {VendorDetailsComponent} from "./vendor-details/vendor-details.component";
import {VendorAddComponent} from "./vendor-add/vendor-add.component";
import {VendorUpdateComponent} from "./vendor-update/vendor-update.component";

export const vendorRoutes: Routes = [
    {
        path: '',
        loadComponent: () => import('./vendor-list/vendor-list.component')
            .then(c => c.VendorListComponent)
    },
    {
        path: 'add',
        loadComponent: () => import('./vendor-add/vendor-add.component')
            .then(c => c.VendorAddComponent)
    },
    {
        path: 'update/:vendorId',
        loadComponent: () => import('./vendor-update/vendor-update.component')
            .then(c => c.VendorUpdateComponent)
    },
    {
        path: ':vendorId',
        loadComponent: () => import('./vendor-details/vendor-details.component')
            .then(c => c.VendorDetailsComponent)
    }
];