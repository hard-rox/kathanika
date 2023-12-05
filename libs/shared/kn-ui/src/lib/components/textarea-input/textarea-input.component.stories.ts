import type { Meta, StoryObj } from '@storybook/angular';
import { TextareaInputComponent } from './textarea-input.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<TextareaInputComponent> = {
  component: TextareaInputComponent,
  title: 'kn-textarea-input',
  tags: ['autodocs']
};
export default meta;
type Story = StoryObj<TextareaInputComponent>;

export const Default: Story = {
  args: {
    label: 'Address'
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/textarea-input works!/gi)).toBeTruthy();
  },
};
