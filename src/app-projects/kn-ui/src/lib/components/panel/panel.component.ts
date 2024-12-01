import {CommonModule} from '@angular/common';
import {Component, input} from '@angular/core';
import {AbstractBlockComponent} from '../../abstractions/abstract-block-component';

@Component({
        selector: 'kn-panel',
    templateUrl: './panel.component.html',
    styleUrls: ['./panel.component.scss'],
    imports: [CommonModule],
})
export class KnPanel extends AbstractBlockComponent {
    readonly panelTitle = input<string | null>(null);

    readonly isLoading = input(false);
}
