import type { Meta, StoryObj } from '@storybook/angular';
import { KnPagination } from './pagination.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<KnPagination> = {
  component: KnPagination,
  title: 'components/kn-pagination',
  tags: ['autodocs']
};
export default meta;
type Story = StoryObj<KnPagination>;

export const Primary: Story = {
  args: {
    totalCount: 100,
    pageSize: 10,
  },
};

export const Heading: Story = {
  args: {
    totalCount: 0,
    pageSize: 0,
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/pagination works!/gi)).toBeTruthy();
  },
};
