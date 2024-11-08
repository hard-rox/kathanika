import type { Meta, StoryObj } from '@storybook/angular';
import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';
import { KnFileInput } from './file-input.component';

const meta: Meta<KnFileInput> = {
    component: KnFileInput,
    title: 'inputs/kn-file-input',
    tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnFileInput>;

export const Primary: Story = {
    args: {},
};

export const Heading: Story = {
    args: {},
    play: async ({ canvasElement }) => {
        const canvas = within(canvasElement);
        expect(canvas.getByText(/date-input works!/gi)).toBeTruthy();
    },
};
