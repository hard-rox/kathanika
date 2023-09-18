import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PublisherListComponent } from './pages/publisher-list/publisher-list.component';
import { PublisherAddComponent } from './pages/publisher-add/publisher-add.component';
import { PublisherUpdateComponent } from './pages/publisher-update/publisher-update.component';

const routes: Routes = [
  {
    path: '',
    component: PublisherListComponent
  },
  {
    path: 'add',
    component: PublisherAddComponent
  },
  {
    path: 'update/:id',
    component: PublisherUpdateComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublishersRoutingModule { }
