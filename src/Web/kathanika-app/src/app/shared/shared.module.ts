import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TableComponent } from './components/table/table.component';
import { PropertyValueGetterPipe } from './pipes/property-value-getter.pipe';
import { PaginationComponent } from './components/pagination/pagination.component';

@NgModule({
  declarations: [TableComponent, PropertyValueGetterPipe, PaginationComponent],
  exports: [TableComponent, PaginationComponent],
  imports: [CommonModule],
})
export class SharedModule {}
