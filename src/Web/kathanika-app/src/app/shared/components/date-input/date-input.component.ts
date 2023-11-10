import { Component, Inject, Injector, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseInputComponent } from '../../bases/base-input-component';

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
      useExisting: DateInputComponent
    }
  ]
})
export class DateInputComponent extends BaseInputComponent<Date> implements OnInit{

  @Input()
  set labelValue(value: string) {
    this.label = value;
  }

  constructor(@Inject(Injector) injector: Injector) {
    super(injector);
  }

  ngOnInit(): void {
    this.init();
  }
}
