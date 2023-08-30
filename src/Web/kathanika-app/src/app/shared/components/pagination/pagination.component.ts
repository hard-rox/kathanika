import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  Component,
  EventEmitter,
  Input,
  Output,
} from '@angular/core';

@Component({
  standalone: true,
  selector: 'kn-pagination',
  templateUrl: './pagination.component.html',
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush,
  imports: [CommonModule]
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
    this.lastPage = totalCount ? Math.ceil(totalCount / this._pageSize) : 1;
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

  @Input('pageSize')
  get pageSize(): number {
    return this._pageSize;
  }
  set pageSize(pageSize: number) {
    this._pageSize = pageSize;
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

  onPageSizeChanged(element: EventTarget | null) {
    let selectedPageSize = +(element as HTMLInputElement).value ?? 0;
    if (selectedPageSize > 0 && this.pageSizes.includes(selectedPageSize)) {
      this._pageSize = selectedPageSize;
      this.currentPage = 1;
      this.lastPage = Math.ceil(this._totalCount / selectedPageSize);
      this.pageSizeChanged.emit(selectedPageSize);
    }
  }
}
