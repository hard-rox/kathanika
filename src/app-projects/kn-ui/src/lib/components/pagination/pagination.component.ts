import {CommonModule} from '@angular/common';
import {ChangeDetectionStrategy, Component, EventEmitter, Input, Output,} from '@angular/core';
import {KnButton} from '../../directives/button/button.directive';
import {AbstractBlockComponent} from '../../abstractions/abstract-block-component';

@Component({
        selector: 'kn-pagination',
    templateUrl: './pagination.component.html',
    styles: [],
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [CommonModule, KnButton],
})
export class KnPagination extends AbstractBlockComponent {
    private _totalCount = 0;
    private _pageSize = 1;
    private _pageSizes: number[] = [5, 10, 20, 50, 100];

    @Input({required: true})
    set totalCount(totalCount: number) {
        this._totalCount = totalCount;
        this.lastPage = totalCount > 0 ? Math.ceil(totalCount / this._pageSize) : 1;
    }

    @Input()
    get pageSizes() {
        return this._pageSizes;
    }

    set pageSizes(pageSizes: number[]) {
        this._pageSizes = pageSizes.slice(0, 4);
        this._pageSize = this._pageSizes[0] ?? 1;
        this.lastPage = Math.ceil(this._totalCount / this._pageSize);
    }

    @Input({required: true})
    get pageSize() {
        return this._pageSize;
    }

    set pageSize(pageSize: number) {
        this._pageSize = pageSize;
        this.lastPage = Math.ceil(this._totalCount / this._pageSize);
    }

    @Output()
    private readonly pageChanged: EventEmitter<number> = new EventEmitter<number>();

    @Output()
    private readonly pageSizeChanged: EventEmitter<number> = new EventEmitter<number>();

    currentPage = 1;
    lastPage = 1;

    protected onPageChanged(pageNumber: number) {
        if (pageNumber >= 1 && pageNumber <= this.lastPage) {
            this.pageChanged.emit(pageNumber);
            this.currentPage = pageNumber;
        }
    }

    protected onPageSizeChanged(element: EventTarget | null) {
        const elementValue = +(element as HTMLInputElement).value;
        const selectedPageSize = elementValue ?? 0;
        if (selectedPageSize > 0 && this.pageSizes.includes(selectedPageSize)) {
            this._pageSize = selectedPageSize;
            this.currentPage = 1;
            this.lastPage = Math.ceil(this._totalCount / selectedPageSize);
            this.pageSizeChanged.emit(selectedPageSize);
        }
    }
}
