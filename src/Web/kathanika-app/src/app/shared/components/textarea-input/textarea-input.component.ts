import { Component, Inject, Injector, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseInputComponent } from '../../bases/base-input-component';

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
      useExisting: TextareaInputComponent
    }
  ]
})
export class TextareaInputComponent extends BaseInputComponent<string> implements OnInit {

  @Input('label')
  set labelValue(value: string) {
    this.label = value;
  }

  @Input('placeholder')
  set placeholderValue(value: string) {
    this.placeholder = value;
  }

  constructor(@Inject(Injector) injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {
    this.init();
  }
}
