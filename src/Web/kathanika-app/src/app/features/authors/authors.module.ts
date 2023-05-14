import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorsRoutingModule } from './authors-routing.module';
import { AuthorListComponent } from './pages/author-list/author-list.component';
import { AuthorDetailsComponent } from './pages/author-details/author-details.component';
import { PaginationModule } from 'src/app/shared/modules/pagination/pagination.module';


@NgModule({
  declarations: [
    AuthorListComponent,
    AuthorDetailsComponent
  ],
  imports: [
    CommonModule,
    AuthorsRoutingModule,
    PaginationModule
  ]
})
export class AuthorsModule { }
