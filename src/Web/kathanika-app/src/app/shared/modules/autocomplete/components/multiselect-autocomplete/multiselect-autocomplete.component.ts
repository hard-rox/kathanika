import { AfterContentInit, Component, ContentChildren, ElementRef, EventEmitter, Inject, Injector, Input, OnInit, Output, QueryList, ViewChild } from '@angular/core';
import { BaseInputComponent } from '../../../../bases/base-input-component';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { AutocompleteOptionComponent } from '../autocomplete-option/autocomplete-option.component';
import { Subject, debounceTime, map } from 'rxjs';

@Component({
  selector: 'kn-multiselect-autocomplete',
  templateUrl: './multiselect-autocomplete.component.html',
  styleUrls: ['./multiselect-autocomplete.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: MultiselectAutocompleteComponent
    }
  ]
})
export class MultiselectAutocompleteComponent
  extends BaseInputComponent<string[]>
  implements OnInit, AfterContentInit {

  @Input('label')
  set labelValue(value: string) {
    this.label = value;
  }

  @Input('placeholder')
  set placeholderValue(value: string) {
    this.placeholder = value;
  }

  @Output('searchTextChanged') searchTextChanged: EventEmitter<string> = new EventEmitter<string>();

  @ContentChildren(AutocompleteOptionComponent) protected options: QueryList<AutocompleteOptionComponent>
    = new QueryList<AutocompleteOptionComponent>(false);

  private _searchInputSubject: Subject<string> = new Subject<string>();

  protected searchInputValue: string = '';
  protected selectedOptions: AutocompleteOptionComponent[] = [];

  constructor(@Inject(Injector) injector: Injector) {
    super(injector);
  }

  private addSubscriberToOptions() {
    this.options
      .filter(opt => !opt.selected.observed)
      .map(opt => {
        opt.selected.subscribe({
          next: (selectedOptionValue: string) => {
            if (this.selectedOptions?.findIndex(x => x.value == selectedOptionValue) >= 0)
              return;

            this.selectedOptions.push(opt);
            this.onModelChange(this.selectedOptions.map(x => x.value ?? ''));
          }
        });
      });
  }

  ngOnInit(): void {
    this.init();
    this.value = [];

    this._searchInputSubject.pipe(
      debounceTime(500),
      map(searchInputValue => {
        this.searchTextChanged.emit(searchInputValue);
      })).subscribe();
  }

  ngAfterContentInit(): void {
    this.addSubscriberToOptions();
    this.options.changes.subscribe(_ => {
      this.addSubscriberToOptions();
    });
  }

  protected onInputChange() {
    this._searchInputSubject.next(this.searchInputValue);
  }

  protected removeSelection(item: AutocompleteOptionComponent) {
    const itemIndex = this.selectedOptions.indexOf(item);
    if (itemIndex <= -1) return;

    this.selectedOptions.splice(itemIndex, 1);
    this.onModelChange(this.selectedOptions.map(x => x.value ?? ''));
  }
}
