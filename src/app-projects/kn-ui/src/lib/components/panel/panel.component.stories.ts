import type {Meta, StoryObj} from '@storybook/angular';
import {KnPanel} from './panel.component';

const meta: Meta<KnPanel> = {
    component: KnPanel,
    title: 'Components/Panel',
    tags: ['autodocs'],
    argTypes: {
        panelTitle: {
            control: 'text',
            description: 'Optional title displayed in the panel header',
            table: {
                type: {summary: 'string | null'},
                defaultValue: {summary: 'null'},
            },
        },
        isLoading: {
            control: 'boolean',
            description: 'Shows a loading overlay when true',
            table: {
                type: {summary: 'boolean'},
                defaultValue: {summary: 'false'},
            },
        },
    },
};
export default meta;
type Story = StoryObj<KnPanel>;

/**
 * Panel without a title header
 */
export const Default: Story = {
    args: {
        panelTitle: null,
        isLoading: false,
    },
    render: (args) => ({
        props: args,
        template: `
            <kn-panel [panelTitle]="panelTitle" [isLoading]="isLoading">
                <p>This is the panel content without a header.</p>
                <p class="mt-2">You can put any content here.</p>
            </kn-panel>
        `,
    }),
};

/**
 * Panel with a title header
 */
export const WithTitle: Story = {
    args: {
        panelTitle: 'Panel Title',
        isLoading: false,
    },
    render: (args) => ({
        props: args,
        template: `
            <kn-panel [panelTitle]="panelTitle" [isLoading]="isLoading">
                <p>This panel has a title header.</p>
                <p class="mt-2">The title is displayed in a styled header section.</p>
            </kn-panel>
        `,
    }),
};

/**
 * Panel in loading state
 */
export const Loading: Story = {
    args: {
        panelTitle: 'Loading Data',
        isLoading: true,
    },
    render: (args) => ({
        props: args,
        template: `
            <kn-panel [panelTitle]="panelTitle" [isLoading]="isLoading">
                <p>This content is hidden behind the loading overlay.</p>
                <p class="mt-2">The loading indicator is visible.</p>
            </kn-panel>
        `,
    }),
};

/**
 * Panel without title in loading state
 */
export const LoadingNoTitle: Story = {
    args: {
        panelTitle: null,
        isLoading: true,
    },
    render: (args) => ({
        props: args,
        template: `
            <kn-panel [panelTitle]="panelTitle" [isLoading]="isLoading">
                <p>Loading content...</p>
            </kn-panel>
        `,
    }),
};

/**
 * Panel with form content
 */
export const WithForm: Story = {
    args: {
        panelTitle: 'User Information',
        isLoading: false,
    },
    render: (args) => ({
        props: args,
        template: `
            <kn-panel [panelTitle]="panelTitle" [isLoading]="isLoading">
                <form class="space-y-4">
                    <div>
                        <label class="block text-sm font-medium mb-1">Name</label>
                        <input type="text" class="w-full border rounded px-3 py-2" placeholder="Enter name">
                    </div>
                    <div>
                        <label class="block text-sm font-medium mb-1">Email</label>
                        <input type="email" class="w-full border rounded px-3 py-2" placeholder="Enter email">
                    </div>
                    <div class="flex justify-end gap-2">
                        <button class="px-4 py-2 border rounded">Cancel</button>
                        <button class="px-4 py-2 bg-blue-500 text-white rounded">Save</button>
                    </div>
                </form>
            </kn-panel>
        `,
    }),
};

/**
 * Panel with data table
 */
export const WithTable: Story = {
    args: {
        panelTitle: 'Recent Orders',
        isLoading: false,
    },
    render: (args) => ({
        props: args,
        template: `
            <kn-panel [panelTitle]="panelTitle" [isLoading]="isLoading">
                <table class="w-full">
                    <thead>
                        <tr class="border-b">
                            <th class="text-left py-2">Order ID</th>
                            <th class="text-left py-2">Customer</th>
                            <th class="text-left py-2">Amount</th>
                            <th class="text-left py-2">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="border-b">
                            <td class="py-2">#1001</td>
                            <td class="py-2">John Doe</td>
                            <td class="py-2">$99.99</td>
                            <td class="py-2">Completed</td>
                        </tr>
                        <tr class="border-b">
                            <td class="py-2">#1002</td>
                            <td class="py-2">Jane Smith</td>
                            <td class="py-2">$149.99</td>
                            <td class="py-2">Pending</td>
                        </tr>
                    </tbody>
                </table>
            </kn-panel>
        `,
    }),
};

/**
 * Multiple panels in a layout
 */
export const MultiplePanels: Story = {
    render: () => ({
        template: `
            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
                <kn-panel panelTitle="Statistics" [isLoading]="false">
                    <div class="space-y-2">
                        <div class="flex justify-between">
                            <span>Total Users:</span>
                            <strong>1,234</strong>
                        </div>
                        <div class="flex justify-between">
                            <span>Active Sessions:</span>
                            <strong>456</strong>
                        </div>
                    </div>
                </kn-panel>
                
                <kn-panel panelTitle="Quick Actions" [isLoading]="false">
                    <div class="space-y-2">
                        <button class="w-full px-4 py-2 bg-blue-500 text-white rounded">New User</button>
                        <button class="w-full px-4 py-2 bg-green-500 text-white rounded">Export Data</button>
                    </div>
                </kn-panel>
            </div>
        `,
    }),
};
