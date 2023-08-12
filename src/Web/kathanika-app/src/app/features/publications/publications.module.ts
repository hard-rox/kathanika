import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicationsRoutingModule } from './publications-routing.module';
import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PaginationModule } from 'src/app/shared/modules/pagination/pagination.module';
import { PublicationAddComponent } from './pages/publication-add/publication-add.component';
import { PublicationUpdateComponent } from './pages/publication-update/publication-update.component';
import { PublicationDetailsComponent } from './pages/publication-details/publication-details.component';


@NgModule({
  declarations: [
    PublicationListComponent,
    PublicationAddComponent,
    PublicationUpdateComponent,
    PublicationDetailsComponent
  ],
  imports: [
    CommonModule,
    PublicationsRoutingModule,
    PaginationModule
  ]
})
export class PublicationsModule { }
