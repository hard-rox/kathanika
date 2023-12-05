import type { Meta, StoryObj } from '@storybook/angular';
import { SearchbarComponent } from './searchbar.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<SearchbarComponent<string>> = {
  component: SearchbarComponent,
  title: 'SearchbarComponent',
};
export default meta;
type Story = StoryObj<SearchbarComponent<string>>;

export const Primary: Story = {
  args: {
    label: null,
    placeholder: 'Search...',
  },
};

export const Heading: Story = {
  args: {
    label: null,
    placeholder: 'Search...',
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/searchbar works!/gi)).toBeTruthy();
  },
};
