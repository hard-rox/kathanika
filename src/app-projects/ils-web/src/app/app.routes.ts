import {Routes} from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'vendors', //TODO: Change this to the default landing page
    },
    {
        path: 'vendors',
        loadChildren: () => import('./features/vendor/vendor.routes')
            .then(x => x.vendorRoutes)
    },
    {
        path: 'purchase-orders',
        loadChildren: () => import('./features/purchase-order/purchase-order.route')
            .then(x => x.purchaseOrderRoutes)
    },
    {
        path: 'cataloging',
        loadChildren: () => import('./features/cataloging/cataloging.routes')
            .then(x => x.catalogingRoutes)
    }
];
