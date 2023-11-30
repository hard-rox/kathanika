import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractInputComponent } from '../../abstractions/base-input-component';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'kn-number-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './number-input.component.html',
  styleUrls: ['./number-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: NumberInputComponent
    }
  ]
})
export class NumberInputComponent extends AbstractInputComponent<number> { }
