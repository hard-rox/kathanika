import type { Meta, StoryObj } from '@storybook/angular';
import { KnSearchbar } from './searchbar.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<KnSearchbar<string>> = {
  component: KnSearchbar,
  title: 'components/kn-searchbar',
  tags: ['autodocs']
};
export default meta;
type Story = StoryObj<KnSearchbar<string>>;

export const Default: Story = {
  args: {
    label: 'Search',
    placeholder: 'Search...',
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/searchbar works!/gi)).toBeTruthy();
  },
};
