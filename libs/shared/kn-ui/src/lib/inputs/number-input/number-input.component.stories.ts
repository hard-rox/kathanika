import type { Meta, StoryObj } from '@storybook/angular';
import { KnNumberInput } from './number-input.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<KnNumberInput> = {
  component: KnNumberInput,
  title: 'inputs/kn-number-input',
  tags: ['autodocs']
};
export default meta;
type Story = StoryObj<KnNumberInput>;

export const Primary: Story = {
  args: {},
};

export const Heading: Story = {
  args: {},
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/number-input works!/gi)).toBeTruthy();
  },
};
