import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, NG_VALUE_ACCESSOR} from '@angular/forms';
import {AbstractInput} from '../../abstractions/abstract-input-component';

@Component({
    selector: 'kn-toggle',
        imports: [CommonModule, FormsModule],
    templateUrl: './toggle.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            multi: true,
            useExisting: KnToggle,
        },
    ],
})
export class KnToggle extends AbstractInput<boolean> {
}
