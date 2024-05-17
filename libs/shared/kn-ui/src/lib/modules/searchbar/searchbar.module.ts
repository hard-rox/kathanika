import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SearchbarComponent } from './components/searchbar/searchbar.component';
import { SearchbarResultComponent } from './components/searchbar-result/searchbar-result.component';

@NgModule({
  declarations: [SearchbarComponent, SearchbarResultComponent],
  imports: [CommonModule, FormsModule],
  exports: [SearchbarComponent, SearchbarResultComponent],
})
export class KnSearchbarModule {}
