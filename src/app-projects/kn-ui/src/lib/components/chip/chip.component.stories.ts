import { argsToTemplate, type Meta, type StoryObj } from '@storybook/angular';
import { KnChip } from './chip.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<KnChip> = {
  component: KnChip,
  title: 'components/kn-chip',
  tags: ['autodocs'],
  // render: (args: KnChip) => ({
  //   props: { ...args },
  //   template: `<kn-chip ${argsToTemplate(args)}>Chip</kn-chip>`,
  // }),
};
export default meta;
type Story = StoryObj<KnChip>;

export const Default: Story = {
  args: {
    key: '1',
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/chip works!/gi)).toBeTruthy();
  },
};
