import type {Meta, StoryObj} from '@storybook/angular';
import {KnToggle} from './toggle.component';
import {FormControl, FormGroup, ReactiveFormsModule} from '@angular/forms';

const meta: Meta<KnToggle> = {
    component: KnToggle,
    title: 'Inputs/Toggle',
    tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnToggle>;

/**
 * Basic toggle switch
 */
export const Default: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                enabled: new FormControl(false),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-toggle 
                    formControlName="enabled"
                    label="Enable Feature"
                    helpText="Turn this feature on or off">
                </kn-toggle>
            </form>
        `,
    }),
};

/**
 * Toggle with initial value
 */
export const EnabledByDefault: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                notifications: new FormControl(true),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-toggle 
                    formControlName="notifications"
                    label="Enable Notifications"
                    helpText="Receive email notifications">
                </kn-toggle>
                <div class="mt-2 text-sm text-gray-600">
                    Status: {{ form.get('notifications')?.value ? 'Enabled' : 'Disabled' }}
                </div>
            </form>
        `,
    }),
};

/**
 * Multiple toggle switches
 */
export const MultipleToggles: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                emailNotifications: new FormControl(true),
                smsNotifications: new FormControl(false),
                pushNotifications: new FormControl(true),
                marketingEmails: new FormControl(false),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form" class="space-y-4">
                <kn-toggle 
                    formControlName="emailNotifications"
                    label="Email Notifications"
                    helpText="Receive notifications via email">
                </kn-toggle>
                
                <kn-toggle 
                    formControlName="smsNotifications"
                    label="SMS Notifications"
                    helpText="Receive notifications via SMS">
                </kn-toggle>
                
                <kn-toggle 
                    formControlName="pushNotifications"
                    label="Push Notifications"
                    helpText="Receive push notifications on your device">
                </kn-toggle>
                
                <kn-toggle 
                    formControlName="marketingEmails"
                    label="Marketing Emails"
                    helpText="Receive promotional and marketing content">
                </kn-toggle>
            </form>
        `,
    }),
};

/**
 * Disabled toggle
 */
export const Disabled: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                locked: new FormControl({value: true, disabled: true}),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-toggle 
                    formControlName="locked"
                    label="Account Locked"
                    helpText="This setting cannot be changed">
                </kn-toggle>
            </form>
        `,
    }),
};
