import {ChangeDetectionStrategy, Component, EventEmitter, Input, Output,} from '@angular/core';
import {CommonModule} from '@angular/common';

@Component({
    selector: 'kn-chip',
    standalone: true,
    imports: [CommonModule],
    templateUrl: './chip.component.html',
    changeDetection: ChangeDetectionStrategy.OnPush,
})
export class KnChip {
    @Input() key: string | undefined;
    @Output() actionPerformed: EventEmitter<string> = new EventEmitter<string>();
}
