import { ChangeDetectionStrategy, Component, Inject, Injector, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR, NgControl, Validators } from '@angular/forms';

@Component({
  selector: 'kn-toggle',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './toggle.component.html',
  styleUrls: ['./toggle.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: ToggleComponent
    }
  ]
})
export class ToggleComponent implements ControlValueAccessor, OnInit {

  @Input('label')
  label: string = '';

  value: boolean = false;
  isDisabled: boolean = false;

  control: NgControl | null = null;

  private onChange = (value: boolean) => { };
  private onTouched = () => { };

  constructor(@Inject(Injector) private injector: Injector) { }

  ngOnInit(): void {
    this.control = this.injector.get(NgControl);
  }

  writeValue(obj: boolean): void {
    this.value = obj;
  }

  registerOnChange(fn: (value: boolean) => void): void {
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
    return true;
  }
}
