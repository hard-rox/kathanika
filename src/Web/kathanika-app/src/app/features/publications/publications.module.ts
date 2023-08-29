import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicationsRoutingModule } from './publications-routing.module';
import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PublicationAddComponent } from './pages/publication-add/publication-add.component';
import { PublicationUpdateComponent } from './pages/publication-update/publication-update.component';
import { PublicationDetailsComponent } from './pages/publication-details/publication-details.component';
import { PublicationFormComponent } from './components/publication-form/publication-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';


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
    PublicationsRoutingModule,
    ReactiveFormsModule,
    PaginationComponent
  ]
})
export class PublicationsModule { }
