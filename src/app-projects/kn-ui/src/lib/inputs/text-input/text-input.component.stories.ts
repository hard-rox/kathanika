import type {Meta, StoryObj} from '@storybook/angular';
import {KnTextInput} from './text-input.component';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';

const meta: Meta<KnTextInput> = {
    component: KnTextInput,
    title: 'Inputs/Text Input',
    tags: ['autodocs'],
    argTypes: {
        label: {
            control: 'text',
            description: 'Label text for the input field',
            table: {
                type: {summary: 'string'},
                defaultValue: {summary: "''"},
            },
        },
        placeholder: {
            control: 'text',
            description: 'Placeholder text when input is empty',
            table: {
                type: {summary: 'string'},
                defaultValue: {summary: "''"},
            },
        },
        helpText: {
            control: 'text',
            description: 'Helper text displayed below the input',
            table: {
                type: {summary: 'string'},
                defaultValue: {summary: "''"},
            },
        },
    },
};
export default meta;
type Story = StoryObj<KnTextInput>;

/**
 * Basic text input with label
 */
export const Default: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                username: new FormControl(''),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-text-input 
                    formControlName="username"
                    label="Username"
                    placeholder="Enter your username">
                </kn-text-input>
            </form>
        `,
    }),
};

/**
 * Text input with help text
 */
export const WithHelpText: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                email: new FormControl(''),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-text-input 
                    formControlName="email"
                    label="Email"
                    placeholder="email@example.com"
                    helpText="We'll never share your email with anyone else.">
                </kn-text-input>
            </form>
        `,
    }),
};

/**
 * Required text input with validation
 */
export const Required: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                name: new FormControl('', Validators.required),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-text-input 
                    formControlName="name"
                    label="Full Name"
                    placeholder="John Doe">
                </kn-text-input>
                <div class="mt-2 text-sm text-gray-600">
                    Value: {{ form.get('name')?.value }}
                </div>
            </form>
        `,
    }),
};

/**
 * Disabled text input
 */
export const Disabled: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                disabled: new FormControl({value: 'Disabled value', disabled: true}),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-text-input 
                    formControlName="disabled"
                    label="Disabled Input"
                    placeholder="Cannot edit">
                </kn-text-input>
            </form>
        `,
    }),
};

/**
 * Multiple text inputs in a form
 */
export const FormExample: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                firstName: new FormControl('', Validators.required),
                lastName: new FormControl('', Validators.required),
                phone: new FormControl(''),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form" class="space-y-4">
                <kn-text-input 
                    formControlName="firstName"
                    label="First Name"
                    placeholder="Enter first name">
                </kn-text-input>
                
                <kn-text-input 
                    formControlName="lastName"
                    label="Last Name"
                    placeholder="Enter last name">
                </kn-text-input>
                
                <kn-text-input 
                    formControlName="phone"
                    label="Phone Number"
                    placeholder="(123) 456-7890"
                    helpText="Optional - Include country code">
                </kn-text-input>
            </form>
        `,
    }),
};
