import type { Meta, StoryObj } from '@storybook/angular';
import { KnBadge } from './badge.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<KnBadge> = {
  component: KnBadge,
  title: 'components/kn-badge',
  tags: ['autodocs']
};
export default meta;
type Story = StoryObj<KnBadge>;

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

export const ErrorBadge: Story = {
  args: {
    content: 'Error',
    type: 'error',
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/badge works!/gi)).toBeTruthy();
  },
};
