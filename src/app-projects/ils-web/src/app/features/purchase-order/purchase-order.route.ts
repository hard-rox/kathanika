import {Routes} from "@angular/router";

export const purchaseOrderRoutes: Routes = [
    {
        path: '',
        loadComponent: () => import('./purchase-order-list/purchase-order-list.component')
            .then(c => c.PurchaseOrderListComponent)
    }
];