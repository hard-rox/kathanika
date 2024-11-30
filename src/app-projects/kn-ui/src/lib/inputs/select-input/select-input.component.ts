import {Component} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule, NG_VALUE_ACCESSOR} from '@angular/forms';
import {AbstractInput} from '../../abstractions/abstract-input-component';

@Component({
    selector: 'kn-select-input',
    
    imports: [CommonModule, FormsModule],
    templateUrl: './select-input.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            multi: true,
            useExisting: KnSelectInput,
        },
    ],
})
export class KnSelectInput extends AbstractInput<string> {
}
