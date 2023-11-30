import { Routes } from "@angular/router";
import { AuthorAddComponent } from "./pages/author-add/author-add.component";
import { AuthorDetailsComponent } from "./pages/author-details/author-details.component";
import { AuthorListComponent } from "./pages/author-list/author-list.component";
import { AuthorUpdateComponent } from "./pages/author-update/author-update.component";

export const routes: Routes = [
  {
    path: '',
    component: AuthorListComponent
  },
  {
    path: 'add',
    component: AuthorAddComponent
  },
  {
    path: 'update/:id',
    component: AuthorUpdateComponent
  },
  {
    path: ':id',
    component: AuthorDetailsComponent
  }
];
