import type {Meta, StoryObj} from '@storybook/angular';
import {KnDateInput} from './date-input.component';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';

const meta: Meta<KnDateInput> = {
    component: KnDateInput,
    title: 'Inputs/Date Input',
    tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnDateInput>;

/**
 * Basic date input
 */
export const Default: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                birthdate: new FormControl(null),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-date-input 
                    formControlName="birthdate"
                    label="Birth Date"
                    placeholder="Select date">
                </kn-date-input>
            </form>
        `,
    }),
};

/**
 * Required date input
 */
export const Required: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                startDate: new FormControl(null, Validators.required),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-date-input 
                    formControlName="startDate"
                    label="Start Date"
                    helpText="Required field">
                </kn-date-input>
                <div class="mt-2 text-sm text-gray-600">
                    Value: {{ form.get('startDate')?.value ?? 'null' }}
                </div>
            </form>
        `,
    }),
};

/**
 * Date range example
 */
export const DateRange: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                startDate: new FormControl(null, Validators.required),
                endDate: new FormControl(null, Validators.required),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form" class="space-y-4">
                <kn-date-input 
                    formControlName="startDate"
                    label="Start Date"
                    helpText="Select start date">
                </kn-date-input>
                
                <kn-date-input 
                    formControlName="endDate"
                    label="End Date"
                    helpText="Select end date">
                </kn-date-input>
            </form>
        `,
    }),
};
