import type { Meta, StoryObj } from '@storybook/angular';
import { SearchbarComponent } from './searchbar.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<SearchbarComponent<string>> = {
  component: SearchbarComponent,
  title: 'kn-searchbar',
};
export default meta;
type Story = StoryObj<SearchbarComponent<string>>;

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
