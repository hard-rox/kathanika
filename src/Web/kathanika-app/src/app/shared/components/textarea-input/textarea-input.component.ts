import { Component, Inject, Injector, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, NgControl, Validators } from '@angular/forms';

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
export class TextareaInputComponent implements ControlValueAccessor, OnInit {

  @Input('label')
  label: string = '';

  @Input('placeholder')
  placeholder: string = '';

  value: string | null = null;
  isDisabled: boolean = false;

  control: NgControl | null = null;

  private onChange = (value: string) => { };
  private onTouched = () => { };

  constructor(@Inject(Injector) private injector: Injector) { }

  ngOnInit(): void {
    this.control = this.injector.get(NgControl);
  }

  writeValue(obj: string): void {
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

  isRequired(): boolean {
    return this.control?.control?.hasValidator(Validators.required) ?? false;
  }
}
