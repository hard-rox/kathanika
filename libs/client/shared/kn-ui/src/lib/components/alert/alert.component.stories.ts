import type { Meta, StoryObj } from '@storybook/angular';
import { AlertComponent } from './alert.component';
import { expect } from '@storybook/jest';

const meta: Meta<AlertComponent> = {
  component: AlertComponent,
  title: 'kn-alert',
};
export default meta;
type Story = StoryObj<AlertComponent>;

export const Default: Story = {
  render: () => ({
    template: '<kn-alert>Hello world</kn-alert>',
  }),
  args: {
    closeable: true,
  },
  play: async () => {
    expect(true).toBeTruthy();
  },
};
