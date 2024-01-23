import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { AbstractInputComponent } from '../../abstractions/abstract-input-component';

@Component({
  selector: 'kn-textarea-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './textarea-input.component.html',
  styleUrls: ['./textarea-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: KnTextareaInput,
    },
  ],
})
export class KnTextareaInput extends AbstractInputComponent<string> {}
