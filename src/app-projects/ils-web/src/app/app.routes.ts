import {Routes} from '@angular/router';

export const routes: Routes = [
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
