import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from '@angular/core';
import { Subject, debounceTime, map } from 'rxjs';

@Component({
  selector: 'kn-searchbar',
  templateUrl: './searchbar.component.html',
  styleUrls: ['./searchbar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SearchbarComponent<T> implements OnInit {
  @Input() label: string | null = null;
  @Input() placeholder: string = 'Search...';

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

  protected searchInputValue: string = '';

  protected onInputChange() {
    this._searchInputSubject.next(this.searchInputValue);
  }
}
