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
  @Input('totalCount')
  private totalCount: number = 0;

  @Input('pageSize')
  private pageSize: number = 0;

  @Output('pageChanged')
  private pageChanged: EventEmitter<number> = new EventEmitter<number>();

  currentPage: number = 1;
  lastPage: number = Math.ceil(this.totalCount / this.pageSize);
  pages: number[] = [1, 2, 3, 4, 5];

  constructor() {}

  onPageChanged(pageNumber: number) {
    if (pageNumber >= 1 && pageNumber <= this.lastPage) {
      this.pageChanged.emit(pageNumber);
    }
  }
}
