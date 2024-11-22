import {CommonModule} from '@angular/common';
import {Component, EventEmitter, Input, Output} from '@angular/core';
import {AbstractBlockComponent} from '../../abstractions/abstract-block-component';

@Component({
    standalone: true,
    selector: 'kn-alert',
    templateUrl: './alert.component.html',
    imports: [CommonModule],
})
export class KnAlert extends AbstractBlockComponent {
    @Input()
    closeable = false;

    @Output()
    private readonly closed = new EventEmitter<void>();

    protected close() {
        this.closed.emit();
    }
}
