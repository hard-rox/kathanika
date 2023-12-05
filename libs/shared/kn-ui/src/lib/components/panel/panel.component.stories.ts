import type { Meta, StoryObj } from '@storybook/angular';
import { PanelComponent } from './panel.component';

import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';

const meta: Meta<PanelComponent> = {
  component: PanelComponent,
  title: 'kn-panel',
  tags: ['autodocs']
};
export default meta;
type Story = StoryObj<PanelComponent>;

export const Default: Story = {
  args: {
    panelTitle: null,
    isLoading: false,
  },
};

export const Heading: Story = {
  args: {
    panelTitle: "Panel Title",
    isLoading: false,
  },
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/panel works!/gi)).toBeTruthy();
  },
};
