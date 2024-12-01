import {ChangeDetectionStrategy, Component, input} from '@angular/core';
import {CommonModule} from '@angular/common';

@Component({
    selector: 'kn-badge',
        imports: [CommonModule],
    templateUrl: './badge.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class KnBadge {
    readonly content = input.required<string>();

    readonly type = input<'success' | 'warning' | 'info' | 'error'>('info');
}
