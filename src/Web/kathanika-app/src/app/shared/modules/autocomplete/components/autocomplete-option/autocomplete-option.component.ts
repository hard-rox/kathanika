import { Component, ElementRef, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'kn-autocomplete-option',
  templateUrl: './autocomplete-option.component.html',
  styleUrls: ['./autocomplete-option.component.scss']
})
export class AutocompleteOptionComponent implements OnInit {

  @Input('value') value: string | null = null;

  @Output('selected') selected: EventEmitter<string> = new EventEmitter<string>();

  public content: HTMLElement | null | undefined;

  constructor(private elementRef: ElementRef) { }

  ngOnInit(): void {
    const root = this.elementRef.nativeElement as HTMLElement
    this.content = root.firstChild?.firstChild as HTMLElement;
  }

  protected onOptionSelect() {
    this.selected.emit(this.value ?? undefined);
  }
}
