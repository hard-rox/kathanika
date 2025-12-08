import type {Meta, StoryObj} from '@storybook/angular';
import {KnTextareaInput} from './textarea-input.component';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';

const meta: Meta<KnTextareaInput> = {
    component: KnTextareaInput,
    title: 'Inputs/Textarea Input',
    tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnTextareaInput>;

/**
 * Basic textarea input
 */
export const Default: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                description: new FormControl(''),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-textarea-input 
                    formControlName="description"
                    label="Description"
                    placeholder="Enter description">
                </kn-textarea-input>
            </form>
        `,
    }),
};

/**
 * Textarea with help text
 */
export const WithHelpText: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                comments: new FormControl(''),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-textarea-input 
                    formControlName="comments"
                    label="Comments"
                    placeholder="Enter your comments..."
                    helpText="Maximum 500 characters">
                </kn-textarea-input>
            </form>
        `,
    }),
};

/**
 * Required textarea
 */
export const Required: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                address: new FormControl('', [Validators.required, Validators.minLength(10)]),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-textarea-input 
                    formControlName="address"
                    label="Address"
                    placeholder="Enter your full address">
                </kn-textarea-input>
                <div class="mt-2 text-sm text-gray-600">
                    Characters: {{ form.get('address')?.value?.length ?? 0 }}
                </div>
            </form>
        `,
    }),
};

/**
 * Disabled textarea
 */
export const Disabled: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                terms: new FormControl({value: 'Terms and conditions text that cannot be edited...', disabled: true}),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-textarea-input 
                    formControlName="terms"
                    label="Terms & Conditions"
                    helpText="Read-only">
                </kn-textarea-input>
            </form>
        `,
    }),
};
