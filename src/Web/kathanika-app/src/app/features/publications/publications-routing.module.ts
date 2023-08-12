import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PublicationAddComponent } from './pages/publication-add/publication-add.component';
import { PublicationDetailsComponent } from './pages/publication-details/publication-details.component';
import { PublicationUpdateComponent } from './pages/publication-update/publication-update.component';

const routes: Routes = [
  {
    path: '',
    component: PublicationListComponent
  },
  {
    path: 'add',
    component: PublicationAddComponent
  },
  {
    path: 'update/:id',
    component: PublicationUpdateComponent
  },
  {
    path: ':id',
    component: PublicationDetailsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicationsRoutingModule { }
