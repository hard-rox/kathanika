import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MultiselectAutocompleteComponent } from './components/multiselect-autocomplete/multiselect-autocomplete.component';
import { AutocompleteOptionComponent } from './components/autocomplete-option/autocomplete-option.component';

@NgModule({
  declarations: [
    MultiselectAutocompleteComponent,
    AutocompleteOptionComponent
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    MultiselectAutocompleteComponent,
    AutocompleteOptionComponent
  ]
})
export class AutocompleteModule { }
