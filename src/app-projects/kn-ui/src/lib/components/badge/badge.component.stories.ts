import type {Meta, StoryObj} from '@storybook/angular';
import {KnBadge} from './badge.component';

const meta: Meta<KnBadge> = {
    component: KnBadge,
    title: 'Components/Badge',
    tags: ['autodocs'],
    argTypes: {
        content: {
            control: 'text',
            description: 'The text content displayed in the badge',
            table: {
                type: {summary: 'string'},
            },
        },
        type: {
            control: 'select',
            options: ['success', 'warning', 'info', 'error'],
            description: 'The type/color variant of the badge',
            table: {
                type: {summary: "'success' | 'warning' | 'info' | 'error'"},
                defaultValue: {summary: "'info'"},
            },
        },
    },
};
export default meta;
type Story = StoryObj<KnBadge>;

/**
 * Default info badge - used for neutral information
 */
export const Info: Story = {
    args: {
        content: 'Info',
        type: 'info',
    },
};

/**
 * Success badge - used for positive states or successful operations
 */
export const Success: Story = {
    args: {
        content: 'Success',
        type: 'success',
    },
};

/**
 * Warning badge - used for warnings or caution states
 */
export const Warning: Story = {
    args: {
        content: 'Warning',
        type: 'warning',
    },
};

/**
 * Error badge - used for errors or critical states
 */
export const Error: Story = {
    args: {
        content: 'Error',
        type: 'error',
    },
};

/**
 * Example showing all badge variants together
 */
export const AllVariants: Story = {
    render: () => ({
        template: `
            <div class="flex gap-2 flex-wrap">
                <kn-badge content="New" type="info"></kn-badge>
                <kn-badge content="Active" type="success"></kn-badge>
                <kn-badge content="Pending" type="warning"></kn-badge>
                <kn-badge content="Failed" type="error"></kn-badge>
            </div>
        `,
    }),
};

/**
 * Numerical badges - common use case for counts
 */
export const WithNumbers: Story = {
    render: () => ({
        template: `
            <div class="flex gap-2 items-center flex-wrap">
                <span>Messages</span>
                <kn-badge content="3" type="error"></kn-badge>
                
                <span class="ml-4">Notifications</span>
                <kn-badge content="12" type="info"></kn-badge>
                
                <span class="ml-4">Completed</span>
                <kn-badge content="99+" type="success"></kn-badge>
            </div>
        `,
    }),
};

/**
 * Status badges - common use case for showing states
 */
export const StatusExamples: Story = {
    render: () => ({
        template: `
            <div class="flex flex-col gap-3">
                <div class="flex items-center gap-2">
                    <span class="w-24">Online:</span>
                    <kn-badge content="Online" type="success"></kn-badge>
                </div>
                <div class="flex items-center gap-2">
                    <span class="w-24">Offline:</span>
                    <kn-badge content="Offline" type="error"></kn-badge>
                </div>
                <div class="flex items-center gap-2">
                    <span class="w-24">Away:</span>
                    <kn-badge content="Away" type="warning"></kn-badge>
                </div>
                <div class="flex items-center gap-2">
                    <span class="w-24">Draft:</span>
                    <kn-badge content="Draft" type="info"></kn-badge>
                </div>
            </div>
        `,
    }),
};
