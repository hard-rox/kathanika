import { Component, Inject, Injector, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { BaseInputComponent } from '../../bases/base-input-component';

@Component({
  selector: 'kn-text-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './text-input.component.html',
  styleUrls: ['./text-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: TextInputComponent
    }
  ]
})
export class TextInputComponent extends BaseInputComponent<string> implements OnInit{

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
