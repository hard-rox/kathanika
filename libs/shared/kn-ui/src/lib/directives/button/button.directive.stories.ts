import type { Meta, StoryObj } from '@storybook/angular';
import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';
import { KnButton } from "./button.directive";


const meta: Meta<KnButton> = {
  component: KnButton,
  title: 'directives/knButton',
  tags: ['autodocs'],
  render: (args) => ({
    props: args,
    template: `<button knButton>Hello Button</button>`
  }),
};
export default meta;
type Story = StoryObj<KnButton>;

export const Default: Story = {
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/pagination works!/gi)).toBeTruthy();
  },
};
