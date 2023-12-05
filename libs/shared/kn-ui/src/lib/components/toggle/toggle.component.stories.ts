import type { Meta, StoryObj } from '@storybook/angular';
import { ToggleComponent } from './toggle.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<ToggleComponent> = {
  component: ToggleComponent,
  title: 'kn-toggle',
};
export default meta;
type Story = StoryObj<ToggleComponent>;

export const Default: Story = {
  args: {
    label: 'Switch'
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/toggle works!/gi)).toBeTruthy();
  },
};
