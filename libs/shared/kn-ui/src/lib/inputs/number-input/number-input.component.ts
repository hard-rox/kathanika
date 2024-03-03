import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractInputComponent } from '../../abstractions/abstract-input-component';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'kn-number-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './number-input.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: KnNumberInput,
    },
  ],
})
export class KnNumberInput extends AbstractInputComponent<number> {}
