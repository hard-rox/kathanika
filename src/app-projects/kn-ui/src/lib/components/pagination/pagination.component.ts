import {CommonModule} from '@angular/common';
import {ChangeDetectionStrategy, Component, effect, input, output} from '@angular/core';
import {KnButton} from '../../directives/button/button.directive';
import {AbstractBlockComponent} from '../../abstractions/abstract-block-component';

@Component({
    selector: 'kn-pagination',
    templateUrl: './pagination.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
    imports: [CommonModule, KnButton],
})
export class KnPagination extends AbstractBlockComponent {

    readonly pageSize = input.required<number>();
    readonly totalCount = input.required<number>();
    readonly pageSizes = input<number[]>([5, 10, 20, 50, 100]);
    readonly pageChanged = output<number>();
    readonly pageSizeChanged = output<number>();

    protected currentPage = 1;
    protected lastPage = 1; //TODO: Use signal or clean.

    protected onPageChanged(pageNumber: number) {
        if (pageNumber >= 1 && pageNumber <= this.lastPage) {
            this.pageChanged.emit(pageNumber);
            this.currentPage = pageNumber;
        }
    }

    protected onPageSizeChanged(element: EventTarget | null) {
        const elementValue = +(element as HTMLInputElement).value;
        const selectedPageSize = elementValue ?? 0;
        if (selectedPageSize > 0 && this.pageSizes().includes(selectedPageSize)) {
            this.currentPage = 1;
            this.lastPage = Math.ceil(this.totalCount() / selectedPageSize);
            if(this.totalCount() == 0){
                this.lastPage = this.currentPage;
            }
            this.pageSizeChanged.emit(selectedPageSize);
        }
    }

    constructor() {
        super();
        effect(() => {
            if(this.totalCount() == 0){
                this.lastPage = this.currentPage;
                return;
            }
            this.lastPage = Math.ceil(this.totalCount() / this.pageSize()) ?? 1;
        });
    }
}
