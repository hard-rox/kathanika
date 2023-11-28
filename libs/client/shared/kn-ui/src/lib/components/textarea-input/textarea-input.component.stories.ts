import type { Meta, StoryObj } from '@storybook/angular';
import { TextareaInputComponent } from './textarea-input.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<TextareaInputComponent> = {
  component: TextareaInputComponent,
  title: 'TextareaInputComponent',
};
export default meta;
type Story = StoryObj<TextareaInputComponent>;

export const Primary: Story = {
  args: {},
};

export const Heading: Story = {
  args: {},
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/textarea-input works!/gi)).toBeTruthy();
  },
};
