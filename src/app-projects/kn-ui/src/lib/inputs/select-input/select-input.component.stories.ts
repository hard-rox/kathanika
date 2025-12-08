import type {Meta, StoryObj} from '@storybook/angular';
import {KnSelectInput} from './select-input.component';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';

const meta: Meta<KnSelectInput> = {
    component: KnSelectInput,
    title: 'Inputs/Select Input',
    tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnSelectInput>;

/**
 * Basic select input
 */
export const Default: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                country: new FormControl(''),
            }),
            countries: ['United States', 'Canada', 'United Kingdom', 'Australia', 'Germany'],
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-select-input 
                    formControlName="country"
                    label="Country"
                    placeholder="Select a country">
                    <option value="">Select...</option>
                    @for (country of countries; track country) {
                        <option [value]="country">{{ country }}</option>
                    }
                </kn-select-input>
            </form>
        `,
    }),
};

/**
 * Required select input
 */
export const Required: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                role: new FormControl('', Validators.required),
            }),
            roles: ['Admin', 'Editor', 'Viewer', 'Guest'],
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-select-input 
                    formControlName="role"
                    label="User Role"
                    helpText="Select a role for this user">
                    <option value="">Select a role...</option>
                    @for (role of roles; track role) {
                        <option [value]="role.toLowerCase()">{{ role }}</option>
                    }
                </kn-select-input>
                <div class="mt-2 text-sm text-gray-600">
                    Selected: {{ form.get('role')?.value || 'None' }}
                </div>
            </form>
        `,
    }),
};

/**
 * Select with option groups
 */
export const WithOptgroups: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                fruit: new FormControl(''),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-select-input 
                    formControlName="fruit"
                    label="Choose a fruit"
                    helpText="Grouped by category">
                    <option value="">Select...</option>
                    <optgroup label="Citrus">
                        <option value="orange">Orange</option>
                        <option value="lemon">Lemon</option>
                        <option value="lime">Lime</option>
                    </optgroup>
                    <optgroup label="Berries">
                        <option value="strawberry">Strawberry</option>
                        <option value="blueberry">Blueberry</option>
                        <option value="raspberry">Raspberry</option>
                    </optgroup>
                    <optgroup label="Tropical">
                        <option value="mango">Mango</option>
                        <option value="pineapple">Pineapple</option>
                        <option value="papaya">Papaya</option>
                    </optgroup>
                </kn-select-input>
            </form>
        `,
    }),
};
