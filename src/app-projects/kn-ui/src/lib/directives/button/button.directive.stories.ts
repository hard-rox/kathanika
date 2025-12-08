import {type Meta, type StoryObj} from '@storybook/angular';
import {KnButton} from './button.directive';

const meta: Meta<KnButton> = {
    component: KnButton,
    title: 'Directives/Button',
    tags: ['autodocs'],
    argTypes: {
        rounded: {
            control: 'boolean',
            description: 'Whether the button has rounded corners (pill shape)',
            table: {
                type: {summary: 'boolean'},
                defaultValue: {summary: 'false'},
            },
        },
        color: {
            control: 'select',
            options: ['primary', 'secondary', 'info', 'success', 'warning', 'error'],
            description: 'The color variant of the button',
            table: {
                type: {summary: "'primary' | 'secondary' | 'info' | 'success' | 'warning' | 'error'"},
                defaultValue: {summary: "'primary'"},
            },
        },
        variant: {
            control: 'select',
            options: ['solid', 'outline', 'transparent'],
            description: 'The style variant of the button',
            table: {
                type: {summary: "'solid' | 'outline' | 'transparent'"},
                defaultValue: {summary: "'solid'"},
            },
        },
    },
};
export default meta;
type Story = StoryObj<KnButton>;

/**
 * Default button with primary color
 */
export const Default: Story = {
    render: (args) => ({
        props: args,
        template: `<button kn-button>Default Button</button>`,
    }),
};

/**
 * All solid color variants
 */
export const SolidColors: Story = {
    render: (args) => ({
        props: args,
        template: `
            <div class="flex gap-3 flex-wrap">
                <button kn-button color="primary">Primary</button>
                <button kn-button color="secondary">Secondary</button>
                <button kn-button color="info">Info</button>
                <button kn-button color="success">Success</button>
                <button kn-button color="warning">Warning</button>
                <button kn-button color="error">Error</button>
            </div>
        `,
    }),
};

/**
 * All outline color variants
 */
export const OutlineColors: Story = {
    render: (args) => ({
        props: args,
        template: `
            <div class="flex gap-3 flex-wrap">
                <button kn-button variant="outline" color="primary">Primary</button>
                <button kn-button variant="outline" color="secondary">Secondary</button>
                <button kn-button variant="outline" color="info">Info</button>
                <button kn-button variant="outline" color="success">Success</button>
                <button kn-button variant="outline" color="warning">Warning</button>
                <button kn-button variant="outline" color="error">Error</button>
            </div>
        `,
    }),
};

/**
 * All transparent color variants
 */
export const TransparentColors: Story = {
    render: (args) => ({
        props: args,
        template: `
            <div class="flex gap-3 flex-wrap">
                <button kn-button variant="transparent" color="primary">Primary</button>
                <button kn-button variant="transparent" color="secondary">Secondary</button>
                <button kn-button variant="transparent" color="info">Info</button>
                <button kn-button variant="transparent" color="success">Success</button>
                <button kn-button variant="transparent" color="warning">Warning</button>
                <button kn-button variant="transparent" color="error">Error</button>
            </div>
        `,
    }),
};

/**
 * Disabled button states
 */
export const Disabled: Story = {
    render: (args) => ({
        props: args,
        template: `
            <div class="flex gap-3 flex-wrap">
                <button kn-button disabled>Disabled Primary</button>
                <button kn-button color="secondary" disabled>Disabled Secondary</button>
                <button kn-button variant="outline" disabled>Disabled Outline</button>
                <button kn-button variant="transparent" disabled>Disabled Transparent</button>
            </div>
        `,
    }),
};

/**
 * Rounded button examples
 */
export const Rounded: Story = {
    render: (args) => ({
        props: args,
        template: `
            <div class="flex gap-3 flex-wrap">
                <button kn-button [rounded]="true" color="primary">Rounded Primary</button>
                <button kn-button [rounded]="true" color="success">Rounded Success</button>
                <button kn-button [rounded]="true" variant="outline" color="error">Rounded Outline</button>
            </div>
        `,
    }),
};

/**
 * Icon-only buttons (rounded circular buttons)
 */
export const IconButtons: Story = {
    render: (args) => ({
        props: args,
        template: `
            <div class="flex gap-3 flex-wrap items-center">
                <button kn-button [rounded]="true" color="primary" aria-label="Attachment">
                    <svg height="20" width="20" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
                        <path d="M18.08,12.42,11.9,18.61a4.25,4.25,0,0,1-6-6l8-8a2.57,2.57,0,0,1,3.54,0,2.52,2.52,0,0,1,0,3.54l-6.9,6.89A.75.75,0,1,1,9.42,14l5.13-5.12a1,1,0,0,0-1.42-1.42L8,12.6a2.74,2.74,0,0,0,0,3.89,2.82,2.82,0,0,0,3.89,0l6.89-6.9a4.5,4.5,0,0,0-6.36-6.36l-8,8A6.25,6.25,0,0,0,13.31,20l6.19-6.18a1,1,0,1,0-1.42-1.42Z"/>
                    </svg>
                </button>
                
                <button kn-button [rounded]="true" color="success" aria-label="Check">
                    <svg height="20" width="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M5 13l4 4L19 7" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg>
                </button>
                
                <button kn-button [rounded]="true" color="error" aria-label="Delete">
                    <svg height="20" width="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M6 6L18 18M6 18L18 6" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                    </svg>
                </button>
                
                <button kn-button [rounded]="true" variant="outline" color="info" aria-label="Edit">
                    <svg height="20" width="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M11 4H4a2 2 0 0 0-2 2v14a2 2 0 0 0 2 2h14a2 2 0 0 0 2-2v-7" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                        <path d="M18.5 2.5a2.121 2.121 0 0 1 3 3L12 15l-4 1 1-4 9.5-9.5z" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg>
                </button>
            </div>
        `,
    }),
};

/**
 * Buttons with icons and text
 */
export const WithIcons: Story = {
    render: (args) => ({
        props: args,
        template: `
            <div class="flex gap-3 flex-wrap">
                <button kn-button color="primary">
                    <svg class="mr-2" height="20" width="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M12 5v14M5 12h14" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                    </svg>
                    Add Item
                </button>
                
                <button kn-button color="success">
                    <svg class="mr-2" height="20" width="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M5 13l4 4L19 7" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg>
                    Save
                </button>
                
                <button kn-button color="error">
                    <svg class="mr-2" height="20" width="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
                        <path d="M6 6L18 18M6 18L18 6" stroke="currentColor" stroke-width="2" stroke-linecap="round"/>
                    </svg>
                    Cancel
                </button>
            </div>
        `,
    }),
};

/**
 * Button sizes example (achieved with custom classes)
 */
export const Sizes: Story = {
    render: (args) => ({
        props: args,
        template: `
            <div class="flex gap-3 items-center flex-wrap">
                <button kn-button class="text-xs px-2 py-1">Small</button>
                <button kn-button>Medium (Default)</button>
                <button kn-button class="text-lg px-4 py-3">Large</button>
            </div>
        `,
    }),
};

/**
 * Complete button matrix showing all combinations
 */
export const CompleteMatrix: Story = {
    render: (args) => ({
        props: args,
        template: `
            <div class="space-y-6">
                <div>
                    <h3 class="text-lg font-semibold mb-3">Solid Variant</h3>
                    <div class="flex gap-2 flex-wrap">
                        <button kn-button variant="solid" color="primary">Primary</button>
                        <button kn-button variant="solid" color="secondary">Secondary</button>
                        <button kn-button variant="solid" color="info">Info</button>
                        <button kn-button variant="solid" color="success">Success</button>
                        <button kn-button variant="solid" color="warning">Warning</button>
                        <button kn-button variant="solid" color="error">Error</button>
                    </div>
                </div>
                
                <div>
                    <h3 class="text-lg font-semibold mb-3">Outline Variant</h3>
                    <div class="flex gap-2 flex-wrap">
                        <button kn-button variant="outline" color="primary">Primary</button>
                        <button kn-button variant="outline" color="secondary">Secondary</button>
                        <button kn-button variant="outline" color="info">Info</button>
                        <button kn-button variant="outline" color="success">Success</button>
                        <button kn-button variant="outline" color="warning">Warning</button>
                        <button kn-button variant="outline" color="error">Error</button>
                    </div>
                </div>
                
                <div>
                    <h3 class="text-lg font-semibold mb-3">Transparent Variant</h3>
                    <div class="flex gap-2 flex-wrap">
                        <button kn-button variant="transparent" color="primary">Primary</button>
                        <button kn-button variant="transparent" color="secondary">Secondary</button>
                        <button kn-button variant="transparent" color="info">Info</button>
                        <button kn-button variant="transparent" color="success">Success</button>
                        <button kn-button variant="transparent" color="warning">Warning</button>
                        <button kn-button variant="transparent" color="error">Error</button>
                    </div>
                </div>
            </div>
        `,
    }),
    parameters: {
        docs: {
            description: {
                story: 'A comprehensive view of all button color and variant combinations available in the design system.',
            },
        },
    },
};
