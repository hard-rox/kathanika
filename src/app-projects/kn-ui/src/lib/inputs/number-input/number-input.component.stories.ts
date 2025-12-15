import type {Meta, StoryObj} from '@storybook/angular';
import {KnNumberInput} from './number-input.component';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';

const meta: Meta<KnNumberInput> = {
    component: KnNumberInput,
    title: 'Inputs/Number Input',
    tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnNumberInput>;

/**
 * Basic number input
 */
export const Default: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                quantity: new FormControl(0),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-number-input 
                    formControlName="quantity"
                    label="Quantity"
                    placeholder="Enter quantity">
                </kn-number-input>
            </form>
        `,
    }),
};

/**
 * Number input with help text
 */
export const WithHelpText: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                age: new FormControl(null),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-number-input 
                    formControlName="age"
                    label="Age"
                    placeholder="Enter your age"
                    helpText="Must be 18 or older">
                </kn-number-input>
            </form>
        `,
    }),
};

/**
 * Required number input
 */
export const Required: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                price: new FormControl(null, [Validators.required, Validators.min(0)]),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-number-input 
                    formControlName="price"
                    label="Price"
                    placeholder="0.00">
                </kn-number-input>
                <div class="mt-2 text-sm text-gray-600">
                    Value: {{ form.get('price')?.value ?? 'null' }}
                </div>
            </form>
        `,
    }),
};

/**
 * Multiple number inputs example
 */
export const FormExample: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                width: new FormControl(0),
                height: new FormControl(0),
                depth: new FormControl(0),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form" class="space-y-4">
                <kn-number-input 
                    formControlName="width"
                    label="Width (cm)"
                    placeholder="0">
                </kn-number-input>
                
                <kn-number-input 
                    formControlName="height"
                    label="Height (cm)"
                    placeholder="0">
                </kn-number-input>
                
                <kn-number-input 
                    formControlName="depth"
                    label="Depth (cm)"
                    placeholder="0">
                </kn-number-input>
            </form>
        `,
    }),
};
