import {ChangeDetectionStrategy, Component, input, output} from '@angular/core';
import {CommonModule} from '@angular/common';

@Component({
    selector: 'kn-chip',
    imports: [CommonModule],
    templateUrl: './chip.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class KnChip {
    readonly key = input<string>();
    readonly actionPerformed = output<string>();
}
