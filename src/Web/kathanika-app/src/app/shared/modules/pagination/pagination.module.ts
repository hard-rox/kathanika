import { NgModule } from '@angular/core';
import { PaginationComponent } from './components/pagination/pagination.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [PaginationComponent],
  imports: [CommonModule],
  exports: [PaginationComponent],
})
export class PaginationModule {}
