import type { Meta, StoryObj } from '@storybook/angular';
import { KnPagination } from './pagination.component';

const meta: Meta<KnPagination> = {
  component: KnPagination,
  title: 'components/kn-pagination',
  tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnPagination>;

export const Primary: Story = {
  args: {
    totalCount: 100,
    pageSize: 10,
  },
};
