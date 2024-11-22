import type {Meta, StoryObj} from '@storybook/angular';
import {KnTextareaInput} from './textarea-input.component';

import {within} from '@storybook/testing-library';
import {expect} from '@storybook/jest';

const meta: Meta<KnTextareaInput> = {
    component: KnTextareaInput,
    title: 'inputs/kn-textarea-input',
    tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnTextareaInput>;

export const Default: Story = {
    args: {
        label: 'Address',
    },
    play: async ({canvasElement}) => {
        const canvas = within(canvasElement);
        expect(canvas.getByText(/textarea-input works!/gi)).toBeTruthy();
    },
};
