import { Component, Inject, Injector, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseInputComponent } from '../../bases/base-input-component';
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
export class NumberInputComponent extends BaseInputComponent<number> implements OnInit {
  @Input()
  set labelValue(value: string) {
    this.label = value;
  }

  @Input()
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
