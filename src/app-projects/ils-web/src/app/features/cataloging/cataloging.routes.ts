import {Routes} from "@angular/router";

export const catalogingRoutes: Routes = [
    {
        path: '',
        loadComponent: () => import('./bib-record-list/bib-record-list.component')
            .then(c => c.BibRecordListComponent)
    },
]