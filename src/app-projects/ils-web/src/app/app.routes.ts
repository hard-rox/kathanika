import {Routes} from '@angular/router';

export const routes: Routes = [
    {
        path: 'vendors',
        loadChildren: () => import('./features/vendor/vendor.routes')
            .then(x => x.vendorRoutes)
    }
];
