import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PropertyValueGetterPipe } from './pipes/property-value-getter.pipe';
import { PaginationComponent } from './components/pagination/pagination.component';

@NgModule({
  declarations: [PropertyValueGetterPipe, PaginationComponent],
  exports: [PaginationComponent],
  imports: [CommonModule],
})
export class SharedModule {}
