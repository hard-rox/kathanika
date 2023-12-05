import type { Meta, StoryObj } from '@storybook/angular';
import { TextInputComponent } from './text-input.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<TextInputComponent> = {
  component: TextInputComponent,
  title: 'kn-text-input',
};
export default meta;
type Story = StoryObj<TextInputComponent>;

export const Default: Story = {
  args: {
    label: 'Insert Text'
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/text-input works!/gi)).toBeTruthy();
  },
};
