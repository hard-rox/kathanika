import {Routes} from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'acquisition/vendors',
    pathMatch: 'full'
  },
  {
    path: 'acquisition/vendors',
    loadChildren: () => import('./features/vendor/vendor.routes')
        .then(x => x.vendorRoutes)
  },
  {
    path: 'acquisition/purchase-orders',
    loadChildren: () => import('./features/purchase-order/purchase-order.route')
        .then(x => x.purchaseOrderRoutes)
  },
  {
    path: 'cataloging',
    loadChildren: () => import('./features/cataloging/cataloging.routes')
        .then(x => x.catalogingRoutes)
  }
];
