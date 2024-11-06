import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Subject, debounceTime, map } from 'rxjs';

@Component({
  selector: 'kn-searchbar',
  templateUrl: './searchbar.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [CommonModule, FormsModule],
})
export class KnSearchbar<T> implements OnInit {
  @Input({ required: true }) options: T[] = [];
  @Input() label: string | null = null;
  @Input() placeholder = 'Search...';
  @Input() displayFn: (option: T) => string = (option) => option as string;
  @Input() required = false;
  @Input() hasError = false;
  @Input() isDisabled = false;

  @Output() searchTextChanged: EventEmitter<string> =
    new EventEmitter<string>();
  @Output() resultSelected: EventEmitter<T> = new EventEmitter<T>();

  private _searchInputSubject: Subject<string> = new Subject<string>();

  ngOnInit(): void {
    this._searchInputSubject
      .pipe(
        debounceTime(500),
        map((searchInputValue) => {
          this.searchTextChanged.emit(searchInputValue);
        }),
      )
      .subscribe();
  }

  protected searchInputValue = '';

  protected onInputChange() {
    this._searchInputSubject.next(this.searchInputValue);
  }

  protected selectOption(selectedOption: T) {
    this.resultSelected.emit(selectedOption);
  }
}
