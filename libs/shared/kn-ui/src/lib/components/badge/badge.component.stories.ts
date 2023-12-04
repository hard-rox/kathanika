import type { Meta, StoryObj } from '@storybook/angular';
import { BadgeComponent } from './badge.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<BadgeComponent> = {
  component: BadgeComponent,
  title: 'BadgeComponent',
};
export default meta;
type Story = StoryObj<BadgeComponent>;

export const Primary: Story = {
  args: {
    content: '',
    type: 'info',
  },
};

export const Heading: Story = {
  args: {
    content: '',
    type: 'info',
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/badge works!/gi)).toBeTruthy();
  },
};
