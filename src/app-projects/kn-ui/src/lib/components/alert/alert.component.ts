import {CommonModule} from '@angular/common';
import {Component, EventEmitter, Output, input} from '@angular/core';
import {AbstractBlockComponent} from '../../abstractions/abstract-block-component';

@Component({
        selector: 'kn-alert',
    templateUrl: './alert.component.html',
    imports: [CommonModule],
})
export class KnAlert extends AbstractBlockComponent {
    readonly closeable = input(false);

    @Output()
    private readonly closed = new EventEmitter<void>();

    protected close() {
        this.closed.emit();
    }
}
