import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorsRoutingModule } from './authors-routing.module';
import { AuthorListComponent } from './pages/author-list/author-list.component';
import { AuthorDetailsComponent } from './pages/author-details/author-details.component';
import { AuthorAddComponent } from './pages/author-add/author-add.component';
import { AuthorFormComponent } from './components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthorUpdateComponent } from './pages/author-update/author-update.component';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { AlertComponent } from 'src/app/shared/components/alert/alert.component';
import { PanelComponent } from 'src/app/shared/components/panel/panel.component';
import { TextInputComponent } from 'src/app/shared/components/text-input/text-input.component';
import { DateInputComponent } from 'src/app/shared/components/date-input/date-input.component';
import { TextareaInputComponent } from 'src/app/shared/components/textarea-input/textarea-input.component';


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
    ReactiveFormsModule,
    AuthorsRoutingModule,
    PaginationComponent,
    AlertComponent,
    PanelComponent,
    TextInputComponent,
    DateInputComponent,
    TextareaInputComponent
  ]
})
export class AuthorsModule { }
