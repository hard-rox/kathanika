import { argsToTemplate, type Meta, type StoryObj } from '@storybook/angular';
import { KnAlert } from './alert.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<KnAlert> = {
  component: KnAlert,
  title: 'components/kn-alert',
  tags: ['autodocs'],
  render: (args: KnAlert) => ({
    props: { ...args },
    template: `<kn-alert ${argsToTemplate(args)}>Hello</kn-alert>`
  })
};
export default meta;
type Story = StoryObj<KnAlert>;

export const Default: Story = {
  args: {
    closeable: true,
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/alert works!/gi)).toBeTruthy();
  },
};
