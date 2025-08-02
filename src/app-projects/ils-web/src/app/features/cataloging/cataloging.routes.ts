import {Routes} from "@angular/router";

export const catalogingRoutes: Routes = [
    {
        path: '',
        redirectTo: 'bibs',
        pathMatch: 'full'
    },
    {
        path: 'bibs',
        loadComponent: () => import('./bib-record-list/bib-record-list.component')
            .then(c => c.BibRecordListComponent)
    },
    {
        path: 'bibs/add',
        loadComponent: () => import('./bib-record-create/bib-record-create.component')
            .then(c => c.BibRecordCreateComponent)
    },
    {
        path: 'bibs/:id',
        loadComponent: () => import('./bib-record-details/bib-record-details.component')
            .then(c => c.BibRecordDetailsComponent)
    }
]