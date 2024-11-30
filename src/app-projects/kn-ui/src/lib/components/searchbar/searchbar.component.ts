import {CommonModule} from '@angular/common';
import {ChangeDetectionStrategy, Component, EventEmitter, OnInit, Output, input} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {debounceTime, map, Subject} from 'rxjs';

@Component({
    selector: 'kn-searchbar',
    templateUrl: './searchbar.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
        imports: [CommonModule, FormsModule],
})
export class KnSearchbar<T> implements OnInit {
    readonly options = input.required<T[]>();
    readonly label = input<string | null>(null);
    readonly placeholder = input('Search...');
    readonly displayFn = input<(option: T) => string>((option) => option as string);
    readonly required = input(false);
    readonly hasError = input(false);
    readonly isDisabled = input(false);

    @Output() searchTextChanged: EventEmitter<string> =
        new EventEmitter<string>();
    @Output() resultSelected: EventEmitter<T> = new EventEmitter<T>();

    private readonly _searchInputSubject: Subject<string> = new Subject<string>();

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
