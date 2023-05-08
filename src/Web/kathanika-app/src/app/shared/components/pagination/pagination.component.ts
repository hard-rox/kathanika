import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PaginationComponent {
  private _totalCount: number = 0;
  private _pageSize: number = 1;
  private _pageSizes: number[] = [5, 10, 50, 100];

  @Input('totalCount')
  get totalCount(): number {
    return this._totalCount;
  }
  set totalCount(totalCount: number | undefined) {
    this._totalCount = totalCount ?? 0;
    this.lastPage = Math.ceil(totalCount ?? 0 / this._pageSize);
  }

  @Input('pageSizes')
  get pageSizes(): number[] {
    return this._pageSizes;
  }
  set pageSizes(pageSizes: number[]) {
    this._pageSizes = pageSizes.slice(0, 4);
    this._pageSize = this._pageSizes[0] ?? 1;
    this.lastPage = Math.ceil(this._totalCount / this._pageSize);
  }

  @Output('pageChanged')
  private pageChanged: EventEmitter<number> = new EventEmitter<number>();

  @Output('pageSizeChanged')
  private pageSizeChanged: EventEmitter<number> = new EventEmitter<number>();

  constructor() {}

  currentPage: number = 1;
  lastPage: number = 1;

  onPageChanged(pageNumber: number) {
    if (pageNumber >= 1 && pageNumber <= this.lastPage) {
      this.pageChanged.emit(pageNumber);
      this.currentPage = pageNumber;
    }
  }

  onChangePageSize(element: EventTarget | null) {
    let selectedPageSize = +(element as HTMLInputElement).value ?? 0;
    if (selectedPageSize > 0 && this.pageSizes.includes(selectedPageSize)) {
      this._pageSize = selectedPageSize;
      this.lastPage = Math.ceil(this._totalCount / selectedPageSize);
      this.pageSizeChanged.emit(selectedPageSize);
    }
  }
}
