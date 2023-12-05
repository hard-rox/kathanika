import type { Meta, StoryObj } from '@storybook/angular';
import { BadgeComponent } from './badge.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<BadgeComponent> = {
  component: BadgeComponent,
  title: 'kn-badge',
  tags: ['autodocs']
};
export default meta;
type Story = StoryObj<BadgeComponent>;

export const Info: Story = {
  args: {
    content: 'Info',
    type: 'info',
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/badge works!/gi)).toBeTruthy();
  },
};

export const Success: Story = {
  args: {
    content: 'Success',
    type: 'success',
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/badge works!/gi)).toBeTruthy();
  },
};

export const Warning: Story = {
  args: {
    content: 'Warning',
    type: 'warning',
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/badge works!/gi)).toBeTruthy();
  },
};

export const Error: Story = {
  args: {
    content: 'Error',
    type: 'error',
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/badge works!/gi)).toBeTruthy();
  },
};
