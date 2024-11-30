import {Component, Inject, Injector, OnInit, input} from '@angular/core';
import {ControlValueAccessor, NgControl, Validators} from '@angular/forms';

@Component({
    template: '',
})
export abstract class AbstractInput<TValueType>
    implements ControlValueAccessor, OnInit {
    readonly label = input('');

    readonly placeholder = input('');

    protected value: TValueType | null = null;
    protected isDisabled = false;

    protected control: NgControl | null = null;

    // eslint-disable-next-line @typescript-eslint/no-unused-vars, @typescript-eslint/no-empty-function
    private onChange = (value: TValueType) => {
    };
    // eslint-disable-next-line @typescript-eslint/no-empty-function
    private onTouched = () => {
    };

    constructor(@Inject(Injector) private readonly injector: Injector) {
    }

    ngOnInit() {
        this.control = this.injector.get(NgControl);
    }

    protected onBlur() {
        this.onTouched();
    }

    protected onModelChange(value: TValueType) {
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
        this.onTouched = fn;
    }

    setDisabledState?(isDisabled: boolean): void {
        this.isDisabled = isDisabled;
    }
}
