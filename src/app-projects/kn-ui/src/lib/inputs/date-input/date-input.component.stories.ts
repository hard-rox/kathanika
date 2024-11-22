import type {Meta, StoryObj} from '@storybook/angular';
import {KnDateInput} from './date-input.component';

import {within} from '@storybook/testing-library';
import {expect} from '@storybook/jest';

const meta: Meta<KnDateInput> = {
    component: KnDateInput,
    title: 'inputs/kn-date-input',
    tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnDateInput>;

export const Primary: Story = {
    args: {},
};

export const Heading: Story = {
    args: {},
    play: async ({canvasElement}) => {
        const canvas = within(canvasElement);
        expect(canvas.getByText(/date-input works!/gi)).toBeTruthy();
    },
};
