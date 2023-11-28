import type { Meta, StoryObj } from '@storybook/angular';
import { SearchbarResultComponent } from './searchbar-result.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<SearchbarResultComponent<string>> = {
  component: SearchbarResultComponent,
  title: 'SearchbarResultComponent',
};
export default meta;
type Story = StoryObj<SearchbarResultComponent<string>>;

export const Primary: Story = {
  args: {},
};

export const Heading: Story = {
  args: {},
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/searchbar-result works!/gi)).toBeTruthy();
  },
};
