import {type Meta, type StoryObj} from '@storybook/angular';
import {KnChip} from './chip.component';
import {expect, fn, userEvent, within} from 'storybook/test';

const meta: Meta<KnChip> = {
    component: KnChip,
    title: 'Components/Chip',
    tags: ['autodocs'],
    argTypes: {
        key: {
            control: 'text',
            description: 'Optional unique identifier for the chip, enables the remove button',
            table: {
                type: {summary: 'string'},
            },
        },
        actionPerformed: {
            description: 'Event emitted when the remove button is clicked, returns the key',
            table: {
                type: {summary: 'EventEmitter<string>'},
            },
        },
    },
    args: {
        actionPerformed: fn(),
    },
};
export default meta;
type Story = StoryObj<KnChip>;

/**
 * Basic chip without remove functionality
 */
export const Default: Story = {
    render: (args) => ({
        props: args,
        template: `<kn-chip>Read-only Chip</kn-chip>`,
    }),
};

/**
 * Chip with remove button - has a key assigned
 */
export const Removable: Story = {
    args: {
        key: 'chip-1',
    },
    render: (args) => ({
        props: args,
        template: `<kn-chip [key]="key" (actionPerformed)="actionPerformed($event)">Removable Chip</kn-chip>`,
    }),
    play: async ({args, canvasElement, step}) => {
        const canvas = within(canvasElement);

        await step('Click remove button', async () => {
            const removeButton = canvas.getByRole('button');
            await userEvent.click(removeButton);
            await expect(args.actionPerformed).toHaveBeenCalledWith('chip-1');
        });
    },
};

/**
 * Multiple chips representing tags
 */
export const TagsExample: Story = {
    render: (args) => ({
        props: {
            ...args,
            tags: ['JavaScript', 'TypeScript', 'Angular', 'React'],
            onRemove: (tag: string) => {
                console.log('Removed tag:', tag);
            },
        },
        template: `
            <div class="flex gap-2 flex-wrap">
                @for (tag of tags; track tag) {
                    <kn-chip [key]="tag" (actionPerformed)="onRemove($event)">
                        {{ tag }}
                    </kn-chip>
                }
            </div>
        `,
    }),
};

/**
 * Filter chips example - common use case
 */
export const FilterChips: Story = {
    render: () => ({
        template: `
            <div>
                <h3 class="text-sm font-semibold mb-2">Active Filters:</h3>
                <div class="flex gap-2 flex-wrap">
                    <kn-chip key="status-active" (actionPerformed)="actionPerformed($event)">
                        Status: Active
                    </kn-chip>
                    <kn-chip key="date-range" (actionPerformed)="actionPerformed($event)">
                        Last 7 days
                    </kn-chip>
                    <kn-chip key="category-tech" (actionPerformed)="actionPerformed($event)">
                        Category: Technology
                    </kn-chip>
                </div>
            </div>
        `,
    }),
};

/**
 * User chips with avatars or icons
 */
export const UserChips: Story = {
    render: () => ({
        template: `
            <div>
                <h3 class="text-sm font-semibold mb-2">Assigned Users:</h3>
                <div class="flex gap-2 flex-wrap">
                    <kn-chip key="user-1" (actionPerformed)="actionPerformed($event)">
                        <span class="mr-1">👤</span> John Doe
                    </kn-chip>
                    <kn-chip key="user-2" (actionPerformed)="actionPerformed($event)">
                        <span class="mr-1">👤</span> Jane Smith
                    </kn-chip>
                    <kn-chip key="user-3" (actionPerformed)="actionPerformed($event)">
                        <span class="mr-1">👤</span> Bob Johnson
                    </kn-chip>
                </div>
            </div>
        `,
    }),
};

/**
 * Mixed removable and non-removable chips
 */
export const MixedChips: Story = {
    render: () => ({
        template: `
            <div class="flex gap-2 flex-wrap">
                <kn-chip>Permanent Label</kn-chip>
                <kn-chip key="temp-1" (actionPerformed)="actionPerformed($event)">
                    Removable 1
                </kn-chip>
                <kn-chip>Another Fixed</kn-chip>
                <kn-chip key="temp-2" (actionPerformed)="actionPerformed($event)">
                    Removable 2
                </kn-chip>
            </div>
        `,
    }),
};
