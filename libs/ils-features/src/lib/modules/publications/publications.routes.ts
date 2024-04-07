import { Routes } from '@angular/router';
import { PublicationAddComponent } from './pages/publication-add/publication-add.component';
import { PublicationDetailsComponent } from './pages/publication-details/publication-details.component';
import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PublicationUpdateComponent } from './pages/publication-update/publication-update.component';
import { RecordPurchaseComponent } from './pages/record-purchase/record-purchase.component';

export const routes: Routes = [
  {
    path: '',
    component: PublicationListComponent,
  },
  {
    path: 'add',
    component: PublicationAddComponent,
  },
  {
    path: 'update/:id',
    component: PublicationUpdateComponent,
  },
  {
    path: 'purchase',
    component: RecordPurchaseComponent
  },
  {
    path: ':id',
    component: PublicationDetailsComponent,
  },
];
