import type { Meta, StoryObj } from '@storybook/angular';
import { KnTextInput } from './text-input.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<KnTextInput> = {
    component: KnTextInput,
    title: 'inputs/kn-text-input',
    tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnTextInput>;

export const Default: Story = {
    args: {
        label: 'Insert Text',
    },
    play: async ({ canvasElement }) => {
        const canvas = within(canvasElement);
        expect(canvas.getByText(/text-input works!/gi)).toBeTruthy();
    },
};
