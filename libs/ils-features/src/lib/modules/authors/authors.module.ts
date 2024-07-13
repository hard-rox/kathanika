import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorListComponent } from './pages/author-list/author-list.component';
import { AuthorDetailsComponent } from './pages/author-details/author-details.component';
import { AuthorAddComponent } from './pages/author-add/author-add.component';
import { AuthorFormComponent } from './components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthorUpdateComponent } from './pages/author-update/author-update.component';
import { RouterModule } from '@angular/router';
import { routes } from './authors.routes';
import {
  KnAlert,
  KnButton,
  KnDateInput,
  KnFileInput,
  KnPagination,
  KnPanel,
  KnTextInput,
  KnTextareaInput,
  KnToggle,
} from '@kathanika/kn-ui';

@NgModule({
  declarations: [
    AuthorListComponent,
    AuthorDetailsComponent,
    AuthorAddComponent,
    AuthorFormComponent,
    AuthorUpdateComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    KnPagination,
    KnAlert,
    KnPanel,
    KnTextInput,
    KnDateInput,
    KnTextareaInput,
    KnToggle,
    KnButton,
    KnFileInput
  ],
})
export class AuthorsModule {}
