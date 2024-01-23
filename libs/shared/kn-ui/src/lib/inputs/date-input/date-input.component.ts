import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { AbstractInputComponent } from '../../abstractions/abstract-input-component';

@Component({
  selector: 'kn-date-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './date-input.component.html',
  styleUrls: ['./date-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: KnDateInput,
    },
  ],
})
export class KnDateInput extends AbstractInputComponent<Date> {}
