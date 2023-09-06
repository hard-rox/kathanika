import { Component, Inject, Injector, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, NgControl, Validators } from '@angular/forms';

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
export class DateInputComponent implements ControlValueAccessor, OnInit {

  @Input('label')
  label: string = '';

  value: Date | null = null;
  isDisabled: boolean = false;

  control: NgControl | null = null;

  private onChange = (value: string) => { };
  private onTouched = () => { };

  constructor(@Inject(Injector) private injector: Injector) { }

  ngOnInit(): void {
    this.control = this.injector.get(NgControl);
  }

  writeValue(obj: Date): void {
    this.value = obj;
  }

  registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }

  onBlur() {
    this.onTouched();
  }

  onModelChange(value: any) {
    this.value = value;
    this.onChange(value);
    this.onTouched();
  }

  isRequired(): boolean{
    return this.control?.control?.hasValidator(Validators.required) ?? false;
  }
}
