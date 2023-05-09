import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthorsRoutingModule } from './authors-routing.module';
import { AuthorListComponent } from './pages/author-list/author-list.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { AuthorDetailsComponent } from './pages/author-details/author-details.component';


@NgModule({
  declarations: [
    AuthorListComponent,
    AuthorDetailsComponent
  ],
  imports: [
    CommonModule,
    AuthorsRoutingModule,
    SharedModule
  ]
})
export class AuthorsModule { }
