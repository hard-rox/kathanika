import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicationsRoutingModule } from './publications-routing.module';
import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PaginationModule } from 'src/app/shared/modules/pagination/pagination.module';


@NgModule({
  declarations: [
    PublicationListComponent
  ],
  imports: [
    CommonModule,
    PublicationsRoutingModule,
    PaginationModule
  ]
})
export class PublicationsModule { }
