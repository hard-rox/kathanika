import {CommonModule} from '@angular/common';
import {Component, Input} from '@angular/core';
import {AbstractBlockComponent} from '../../abstractions/abstract-block-component';

@Component({
    standalone: true,
    selector: 'kn-panel',
    templateUrl: './panel.component.html',
    styleUrls: ['./panel.component.scss'],
    imports: [CommonModule],
})
export class KnPanel extends AbstractBlockComponent {
    @Input()
    panelTitle: string | null = null;

    @Input()
    isLoading = false;
}
