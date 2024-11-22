import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, NG_VALUE_ACCESSOR} from '@angular/forms';
import {AbstractInput} from '../../abstractions/abstract-input-component';

@Component({
    selector: 'kn-text-input',
    standalone: true,
    imports: [CommonModule, FormsModule],
    templateUrl: './text-input.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            multi: true,
            useExisting: KnTextInput,
        },
    ],
})
export class KnTextInput extends AbstractInput<string> {
}
