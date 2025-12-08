// noinspection ES6PreferShortImport

import {type Meta, type StoryObj} from '@storybook/angular';
import {KnAlert} from './alert.component';
import {expect, fn, userEvent, within} from 'storybook/test';

const meta: Meta<KnAlert> = {
    component: KnAlert,
    title: 'Components/Alert',
    tags: ['autodocs'],
    argTypes: {
        closeable: {
            control: 'boolean',
            description: 'Whether the alert can be closed by the user',
            table: {
                type: {summary: 'boolean'},
                defaultValue: {summary: 'false'},
            },
        },
        closed: {
            description: 'Event emitted when the close button is clicked',
            table: {
                type: {summary: 'EventEmitter<void>'},
            },
        },
    },
    args: {
        closed: fn(),
    },
    render: (args) => ({
        props: args,
        template: `<kn-alert [closeable]="closeable" (closed)="closed()">
            <ng-content></ng-content>
        </kn-alert>`,
    }),
};
export default meta;
type Story = StoryObj<KnAlert>;

/**
 * Default alert with informational message
 */
export const Default: Story = {
    args: {
        closeable: false,
    },
    render: (args) => ({
        props: args,
        template: `<kn-alert [closeable]="closeable" (closed)="closed()">
            This is an important notification message.
        </kn-alert>`,
    }),
};

/**
 * Alert with close button that can be dismissed
 */
export const Closeable: Story = {
    args: {
        closeable: true,
    },
    render: (args) => ({
        props: args,
        template: `<kn-alert [closeable]="closeable" (closed)="closed()">
            You can dismiss this alert by clicking the close button.
        </kn-alert>`,
    }),
    play: async ({args, canvasElement, step}) => {
        const canvas = within(canvasElement);

        await step('Verify alert is visible', async () => {
            const alert = canvas.getByText(/You can dismiss this alert/i);
            await expect(alert).toBeTruthy();
        });

        await step('Click close button', async () => {
            const closeButton = canvas.getByRole('button');
            await userEvent.click(closeButton);
            await expect(args.closed).toHaveBeenCalledTimes(1);
        });
    },
};

/**
 * Error alert example
 */
export const ErrorMessage: Story = {
    args: {
        closeable: true,
    },
    render: (args) => ({
        props: args,
        template: `<kn-alert [closeable]="closeable" (closed)="closed()">
            <strong>Error:</strong> Failed to save changes. Please try again.
        </kn-alert>`,
    }),
};

/**
 * Success alert example
 */
export const SuccessMessage: Story = {
    args: {
        closeable: true,
    },
    render: (args) => ({
        props: args,
        template: `<kn-alert [closeable]="closeable" (closed)="closed()">
            <strong>Success!</strong> Your changes have been saved successfully.
        </kn-alert>`,
    }),
};

/**
 * Warning alert example
 */
export const WarningMessage: Story = {
    args: {
        closeable: false,
    },
    render: (args) => ({
        props: args,
        template: `<kn-alert [closeable]="closeable" (closed)="closed()">
            <strong>Warning:</strong> This action cannot be undone.
        </kn-alert>`,
    }),
};

/**
 * Alert with complex content including list
 */
export const ComplexContent: Story = {
    args: {
        closeable: true,
    },
    render: (args) => ({
        props: args,
        template: `<kn-alert [closeable]="closeable" (closed)="closed()">
            <div>
                <strong>Please review the following:</strong>
                <ul class="list-disc list-inside mt-2">
                    <li>Your profile is incomplete</li>
                    <li>Email verification pending</li>
                    <li>Payment method not configured</li>
                </ul>
            </div>
        </kn-alert>`,
    }),
};
