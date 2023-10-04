import { Inject, Injector } from "@angular/core";
import { ControlValueAccessor, NgControl, Validators } from "@angular/forms";

export abstract class BaseInputComponent<TValueType> implements ControlValueAccessor {
  protected label: string = '';
  protected placeholder: string = '';

  protected value: TValueType | null = null;
  protected isDisabled: boolean = false;

  protected control: NgControl | null = null;

  private onChange = (value: TValueType) => { };
  private onTouched = () => { };

  constructor(@Inject(Injector) private injector: Injector) { }

  protected init() {
    this.control = this.injector.get(NgControl);
  }

  protected onBlur() {
    this.onTouched();
  }

  protected onModelChange(value: any) {
    this.value = value;
    this.onChange(value);
    this.onTouched();
  }

  protected isRequired(): boolean {
    return this.control?.control?.hasValidator(Validators.required) ?? false;
  }

  writeValue(obj: TValueType): void {
    this.value = obj;
  }

  registerOnChange(fn: (value: TValueType) => void): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }
}
