import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthorListComponent } from './pages/author-list/author-list.component';
import { AuthorDetailsComponent } from './pages/author-details/author-details.component';
import { AuthorAddComponent } from './pages/author-add/author-add.component';

const routes: Routes = [
  {
    path: '',
    component: AuthorListComponent
  },
  {
    path: 'add',
    component: AuthorAddComponent
  },
  {
    path: ':id',
    component: AuthorDetailsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthorsRoutingModule { }
