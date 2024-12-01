import {CommonModule} from '@angular/common';
import {Component, input, output} from '@angular/core';
import {AbstractBlockComponent} from '../../abstractions/abstract-block-component';

@Component({
        selector: 'kn-alert',
    templateUrl: './alert.component.html',
    imports: [CommonModule],
})
export class KnAlert extends AbstractBlockComponent {
    readonly closeable = input(false);
    readonly closed = output<void>();

    protected close() {
        this.closed.emit();
    }
}
