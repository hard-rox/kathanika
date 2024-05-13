import { Routes } from '@angular/router';
import { PublicationAcquireComponent } from './pages/publication-acquire/publication-acquire.component';
import { PublicationDetailsComponent } from './pages/publication-details/publication-details.component';
import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PublicationUpdateComponent } from './pages/publication-update/publication-update.component';

export const routes: Routes = [
  {
    path: '',
    component: PublicationListComponent,
  },
  {
    path: 'acquire',
    component: PublicationAcquireComponent,
  },
  {
    path: 'update/:id',
    component: PublicationUpdateComponent,
  },
  {
    path: ':id',
    component: PublicationDetailsComponent,
  },
];
