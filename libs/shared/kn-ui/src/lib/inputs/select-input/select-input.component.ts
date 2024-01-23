import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { AbstractInputComponent } from '../../abstractions/abstract-input-component';

@Component({
  selector: 'kn-select-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './select-input.component.html',
  styleUrls: ['./select-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: KnSelectInput,
    },
  ],
})
export class KnSelectInput extends AbstractInputComponent<string> {}
