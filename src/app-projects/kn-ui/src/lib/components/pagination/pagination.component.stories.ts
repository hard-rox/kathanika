import type {Meta, StoryObj} from '@storybook/angular';
import {KnPagination} from './pagination.component';
import {fn} from 'storybook/test';

const meta: Meta<KnPagination> = {
    component: KnPagination,
    title: 'Components/Pagination',
    tags: ['autodocs'],
    argTypes: {
        pageSize: {
            control: 'number',
            description: 'Number of items per page',
            table: {
                type: {summary: 'number'},
            },
        },
        totalCount: {
            control: 'number',
            description: 'Total number of items',
            table: {
                type: {summary: 'number'},
            },
        },
        pageSizes: {
            control: 'object',
            description: 'Available page size options',
            table: {
                type: {summary: 'number[]'},
                defaultValue: {summary: '[5, 10, 20, 50, 100]'},
            },
        },
        pageChanged: {
            description: 'Event emitted when page number changes',
            table: {
                type: {summary: 'EventEmitter<number>'},
            },
        },
        pageSizeChanged: {
            description: 'Event emitted when page size changes',
            table: {
                type: {summary: 'EventEmitter<number>'},
            },
        },
    },
    args: {
        pageChanged: fn(),
        pageSizeChanged: fn(),
    },
};
export default meta;
type Story = StoryObj<KnPagination>;

/**
 * Default pagination with 100 total items
 */
export const Default: Story = {
    args: {
        totalCount: 100,
        pageSize: 10,
    },
};

/**
 * Pagination with small dataset
 */
export const SmallDataset: Story = {
    args: {
        totalCount: 25,
        pageSize: 5,
    },
};

/**
 * Pagination with large dataset
 */
export const LargeDataset: Story = {
    args: {
        totalCount: 1000,
        pageSize: 20,
    },
};

/**
 * Custom page size options
 */
export const CustomPageSizes: Story = {
    args: {
        totalCount: 200,
        pageSize: 25,
        pageSizes: [10, 25, 50, 100],
    },
};

/**
 * Empty state - no items to paginate
 */
export const EmptyState: Story = {
    args: {
        totalCount: 0,
        pageSize: 10,
    },
};

/**
 * Single page - all items fit on one page
 */
export const SinglePage: Story = {
    args: {
        totalCount: 8,
        pageSize: 10,
    },
};

/**
 * Pagination integrated with a data table example
 */
export const WithDataTable: Story = {
    args: {
        totalCount: 50,
        pageSize: 10,
    },
    render: (args) => ({
        props: args,
        template: `
            <div class="space-y-4">
                <div class="border rounded">
                    <table class="w-full">
                        <thead class="bg-gray-100">
                            <tr>
                                <th class="px-4 py-2 text-left">ID</th>
                                <th class="px-4 py-2 text-left">Name</th>
                                <th class="px-4 py-2 text-left">Email</th>
                                <th class="px-4 py-2 text-left">Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            @for (i of [1,2,3,4,5,6,7,8,9,10]; track i) {
                                <tr class="border-t">
                                    <td class="px-4 py-2">{{ i }}</td>
                                    <td class="px-4 py-2">User {{ i }}</td>
                                    <td class="px-4 py-2">user{{ i }}@example.com</td>
                                    <td class="px-4 py-2">Active</td>
                                </tr>
                            }
                        </tbody>
                    </table>
                </div>
                <kn-pagination 
                    [totalCount]="totalCount" 
                    [pageSize]="pageSize"
                    [pageSizes]="pageSizes"
                    (pageChanged)="pageChanged($event)"
                    (pageSizeChanged)="pageSizeChanged($event)">
                </kn-pagination>
            </div>
        `,
    }),
};
