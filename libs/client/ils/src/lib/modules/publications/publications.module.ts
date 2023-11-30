import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PublicationAddComponent } from './pages/publication-add/publication-add.component';
import { PublicationUpdateComponent } from './pages/publication-update/publication-update.component';
import { PublicationDetailsComponent } from './pages/publication-details/publication-details.component';
import { PublicationFormComponent } from './components/publication-form/publication-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { routes } from './publications.routes';
import {
  AlertComponent,
  ChipComponent,
  DateInputComponent,
  NumberInputComponent,
  PaginationComponent,
  PanelComponent,
  SearchbarModule,
  SelectInputComponent,
  TextInputComponent,
  TextareaInputComponent
} from '@kathanika/kn-ui';

@NgModule({
  declarations: [
    PublicationListComponent,
    PublicationAddComponent,
    PublicationUpdateComponent,
    PublicationDetailsComponent,
    PublicationFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    PaginationComponent,
    TextInputComponent,
    TextareaInputComponent,
    DateInputComponent,
    AlertComponent,
    PanelComponent,
    SelectInputComponent,
    NumberInputComponent,
    ChipComponent,
    SearchbarModule
  ]
})
export class PublicationsModule { }
