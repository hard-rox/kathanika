import {Routes} from "@angular/router";

export const purchaseOrderRoutes: Routes = [
    {
        path: '',
        loadComponent: () => import('./purchase-order-list/purchase-order-list.component')
            .then(c => c.PurchaseOrderListComponent)
    },
    {
        path: 'create',
        loadComponent: () => import('./purchase-order-create/purchase-order-create.component')
            .then(c => c.PurchaseOrderCreateComponent)
    },
    {
        path: ':purchaseOrderId',
        loadComponent: () => import('./purchase-order-details/purchase-order-details.component')
            .then(c => c.PurchaseOrderDetailsComponent)
    }
];