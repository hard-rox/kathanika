import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'kn-autocomplete-option',
  templateUrl: './autocomplete-option.component.html',
  styleUrls: ['./autocomplete-option.component.scss']
})
export class AutocompleteOptionComponent {

  @Input('value') value: string | null = null;

  @Output('selected') selected: EventEmitter<string> = new EventEmitter<string>();

  onOptionSelect() {
    this.selected.emit(this.value ?? undefined);
  }
}
