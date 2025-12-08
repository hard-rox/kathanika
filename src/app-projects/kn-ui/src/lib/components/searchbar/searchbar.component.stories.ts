import type {Meta, StoryObj} from '@storybook/angular';
import {KnSearchbar} from './searchbar.component';
import {fn} from 'storybook/test';

const meta: Meta<KnSearchbar<string>> = {
    component: KnSearchbar,
    title: 'Components/Searchbar',
    tags: ['autodocs'],
    argTypes: {
        options: {
            control: 'object',
            description: 'Array of options to display in the dropdown',
            table: {
                type: {summary: 'T[]'},
            },
        },
        label: {
            control: 'text',
            description: 'Label text for the searchbar',
            table: {
                type: {summary: 'string | null'},
                defaultValue: {summary: 'null'},
            },
        },
        placeholder: {
            control: 'text',
            description: 'Placeholder text when input is empty',
            table: {
                type: {summary: 'string'},
                defaultValue: {summary: "'Search...'"},
            },
        },
        displayFn: {
            description: 'Function to transform option to display string',
            table: {
                type: {summary: '(option: T) => string'},
            },
        },
        required: {
            control: 'boolean',
            description: 'Whether the field is required',
            table: {
                type: {summary: 'boolean'},
                defaultValue: {summary: 'false'},
            },
        },
        hasError: {
            control: 'boolean',
            description: 'Whether to display error state',
            table: {
                type: {summary: 'boolean'},
                defaultValue: {summary: 'false'},
            },
        },
        isDisabled: {
            control: 'boolean',
            description: 'Whether the input is disabled',
            table: {
                type: {summary: 'boolean'},
                defaultValue: {summary: 'false'},
            },
        },
        searchTextChanged: {
            description: 'Event emitted when search text changes (debounced 500ms)',
            table: {
                type: {summary: 'EventEmitter<string>'},
            },
        },
        resultSelected: {
            description: 'Event emitted when an option is selected',
            table: {
                type: {summary: 'EventEmitter<T>'},
            },
        },
    },
    args: {
        searchTextChanged: fn(),
        resultSelected: fn(),
    },
};
export default meta;
type Story = StoryObj<KnSearchbar<string>>;

/**
 * Basic searchbar with simple string options
 */
export const Default: Story = {
    args: {
        label: 'Search',
        placeholder: 'Search...',
        options: ['Apple', 'Banana', 'Cherry', 'Date', 'Elderberry'],
        required: false,
        hasError: false,
        isDisabled: false,
    },
};

/**
 * Searchbar with a label
 */
export const WithLabel: Story = {
    args: {
        label: 'Search Countries',
        placeholder: 'Type to search...',
        options: ['United States', 'United Kingdom', 'Canada', 'Australia', 'Germany'],
    },
};

/**
 * Required searchbar field
 */
export const Required: Story = {
    args: {
        label: 'Search Product',
        placeholder: 'Search products...',
        options: ['Laptop', 'Mouse', 'Keyboard', 'Monitor', 'Headphones'],
        required: true,
    },
};

/**
 * Searchbar in error state
 */
export const WithError: Story = {
    args: {
        label: 'Search User',
        placeholder: 'Search users...',
        options: ['John Doe', 'Jane Smith', 'Bob Johnson'],
        hasError: true,
        required: true,
    },
};

/**
 * Disabled searchbar
 */
export const Disabled: Story = {
    args: {
        label: 'Search (Disabled)',
        placeholder: 'Disabled search...',
        options: [],
        isDisabled: true,
    },
};

/**
 * Empty results state
 */
export const NoResults: Story = {
    args: {
        label: 'Search',
        placeholder: 'Search...',
        options: [],
    },
};

/**
 * Searchbar with custom object types
 */
export const WithObjects: Story = {
    args: {
        label: 'Search Employee',
        placeholder: 'Search by name...',
        options: [
            {id: 1, name: 'John Doe', department: 'Engineering'},
            {id: 2, name: 'Jane Smith', department: 'Marketing'},
            {id: 3, name: 'Bob Johnson', department: 'Sales'},
            {id: 4, name: 'Alice Williams', department: 'HR'},
        ] as any,
        displayFn: (option: any) => `${option.name} - ${option.department}`,
    },
};

/**
 * Large dataset example
 */
export const LargeDataset: Story = {
    args: {
        label: 'Search City',
        placeholder: 'Type city name...',
        options: [
            'New York', 'Los Angeles', 'Chicago', 'Houston', 'Phoenix',
            'Philadelphia', 'San Antonio', 'San Diego', 'Dallas', 'San Jose',
            'Austin', 'Jacksonville', 'Fort Worth', 'Columbus', 'Indianapolis',
            'Charlotte', 'San Francisco', 'Seattle', 'Denver', 'Washington DC',
        ],
    },
};

/**
 * Interactive example showing all states
 */
export const InteractiveExample: Story = {
    render: (args) => ({
        props: {
            ...args,
            states: [
                {label: 'Normal', hasError: false, isDisabled: false, required: false},
                {label: 'Required', hasError: false, isDisabled: false, required: true},
                {label: 'Error', hasError: true, isDisabled: false, required: false},
                {label: 'Disabled', hasError: false, isDisabled: true, required: false},
            ],
            options: ['Option 1', 'Option 2', 'Option 3', 'Option 4'],
        },
        template: `
            <div class="space-y-4">
                @for (state of states; track state.label) {
                    <kn-searchbar
                        [label]="state.label"
                        [options]="options"
                        [required]="state.required"
                        [hasError]="state.hasError"
                        [isDisabled]="state.isDisabled"
                        [placeholder]="'Search ' + state.label.toLowerCase() + '...'"
                        (searchTextChanged)="searchTextChanged($event)"
                        (resultSelected)="resultSelected($event)">
                    </kn-searchbar>
                }
            </div>
        `,
    }),
};
