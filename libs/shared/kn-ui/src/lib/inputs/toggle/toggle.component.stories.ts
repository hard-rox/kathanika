import type { Meta, StoryObj } from '@storybook/angular';
import { KnToggle } from './toggle.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<KnToggle> = {
  component: KnToggle,
  title: 'inputs/kn-toggle',
  tags: ['autodocs']
};
export default meta;
type Story = StoryObj<KnToggle>;

export const Default: Story = {
  args: {
    label: 'Switch'
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/toggle works!/gi)).toBeTruthy();
  },
};
