import { argsToTemplate, type Meta, type StoryObj } from '@storybook/angular';
import { AlertComponent } from './alert.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<AlertComponent> = {
  component: AlertComponent,
  title: 'kn-alert',
  render: (args: AlertComponent) => ({
    props: { ...args },
    template: `<kn-alert ${argsToTemplate(args)}>Hello</kn-alert>`
  })
};
export default meta;
type Story = StoryObj<AlertComponent>;

export const Default: Story = {
  args: {
    closeable: true,
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/alert works!/gi)).toBeTruthy();
  },
};
