import { Routes } from '@angular/router';
import { PublisherAddComponent } from './pages/publisher-add/publisher-add.component';
import { PublisherListComponent } from './pages/publisher-list/publisher-list.component';
import { PublisherUpdateComponent } from './pages/publisher-update/publisher-update.component';

export const routes: Routes = [
  {
    path: '',
    component: PublisherListComponent,
  },
  {
    path: 'add',
    component: PublisherAddComponent,
  },
  {
    path: 'update/:id',
    component: PublisherUpdateComponent,
  },
];
