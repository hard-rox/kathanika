import { AfterContentInit, Component, ContentChildren, EventEmitter, Inject, Injector, Input, OnInit, Output, QueryList } from '@angular/core';
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

  private _selectedValues: string[] = [];
  private _searchInputSubject: Subject<string> = new Subject<string>();

  protected searchInputValue: string = '';

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

  constructor(@Inject(Injector) injector: Injector) {
    super(injector);
  }

  private addSubscriberToOptions() {
    this.options.map(opt => {
      if (!opt.selected.observed) {
        opt.selected.subscribe({
          next: (selectedOptionValue: string) => {
            if (this._selectedValues?.findIndex(x => x == selectedOptionValue) < 0) {
              this._selectedValues.push(selectedOptionValue);
              this.onModelChange(this._selectedValues);
            }
          }
        });
      }
    });
  }

  protected onInputChange() {
    this._searchInputSubject.next(this.searchInputValue);
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
      // console.debug(_)
      this.addSubscriberToOptions();
    });
  }
}
