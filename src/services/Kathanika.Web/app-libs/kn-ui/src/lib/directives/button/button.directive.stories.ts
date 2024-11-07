import { type Meta, type StoryObj } from '@storybook/angular';
import { within } from '@storybook/testing-library';
import { expect } from '@storybook/jest';
import { KnButton } from './button.directive';

const meta: Meta<KnButton> = {
  component: KnButton,
  title: 'directives/kn-button',
  tags: ['autodocs'],
};
export default meta;
type Story = StoryObj<KnButton>;

export const All: Story = {
  render: (args) => ({
    props: args,
    template: `
      <div class="flex justify-around w-full">
        <button kn-button>Default</button>
        <button kn-button variant="light">Light</button>
        <button kn-button variant="dark">Dark</button>
        <button kn-button variant="dark" [rounded]="true">Rounded</button>
        <button kn-button [rounded]="true" variant="light"><svg height="20" width="20" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path d="M18.08,12.42,11.9,18.61a4.25,4.25,0,0,1-6-6l8-8a2.57,2.57,0,0,1,3.54,0,2.52,2.52,0,0,1,0,3.54l-6.9,6.89A.75.75,0,1,1,9.42,14l5.13-5.12a1,1,0,0,0-1.42-1.42L8,12.6a2.74,2.74,0,0,0,0,3.89,2.82,2.82,0,0,0,3.89,0l6.89-6.9a4.5,4.5,0,0,0-6.36-6.36l-8,8A6.25,6.25,0,0,0,13.31,20l6.19-6.18a1,1,0,1,0-1.42-1.42Z"/></svg></button>
        <button kn-button [rounded]="true" variant="transparent"><svg height="20" width="20" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path d="M18.08,12.42,11.9,18.61a4.25,4.25,0,0,1-6-6l8-8a2.57,2.57,0,0,1,3.54,0,2.52,2.52,0,0,1,0,3.54l-6.9,6.89A.75.75,0,1,1,9.42,14l5.13-5.12a1,1,0,0,0-1.42-1.42L8,12.6a2.74,2.74,0,0,0,0,3.89,2.82,2.82,0,0,0,3.89,0l6.89-6.9a4.5,4.5,0,0,0-6.36-6.36l-8,8A6.25,6.25,0,0,0,13.31,20l6.19-6.18a1,1,0,1,0-1.42-1.42Z"/></svg></button>
      </div>
    `,
  }),
  tags: [''],
};

export const Default: Story = {
  render: (args) => ({
    props: args,
    template: `<button kn-button>Default</button>`,
  }),
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/pagination works!/gi)).toBeTruthy();
  },
};

export const Light: Story = {
  render: (args) => ({
    props: args,
    template: `<button kn-button variant="light">Light</button>`,
  }),
  play: async ({ canvasElement }) => {
    const canvas = within(canvasElement);
    expect(canvas.getByText(/pagination works!/gi)).toBeTruthy();
  },
};

export const Rounded: Story = {
  render: (args) => ({
    props: args,
    template: `<button kn-button [rounded]="true">Rounded Button</button>`,
  }),
};

export const Icon: Story = {
  render: (args) => ({
    props: args,
    template: `<button kn-button [rounded]="true"><svg height="20" width="20" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path d="M18.08,12.42,11.9,18.61a4.25,4.25,0,0,1-6-6l8-8a2.57,2.57,0,0,1,3.54,0,2.52,2.52,0,0,1,0,3.54l-6.9,6.89A.75.75,0,1,1,9.42,14l5.13-5.12a1,1,0,0,0-1.42-1.42L8,12.6a2.74,2.74,0,0,0,0,3.89,2.82,2.82,0,0,0,3.89,0l6.89-6.9a4.5,4.5,0,0,0-6.36-6.36l-8,8A6.25,6.25,0,0,0,13.31,20l6.19-6.18a1,1,0,1,0-1.42-1.42Z"/></svg></button>`,
  }),
};

export const IconLight: Story = {
  render: (args) => ({
    props: args,
    template: `<button kn-button [rounded]="true" variant="light"><svg height="20" width="20" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path d="M18.08,12.42,11.9,18.61a4.25,4.25,0,0,1-6-6l8-8a2.57,2.57,0,0,1,3.54,0,2.52,2.52,0,0,1,0,3.54l-6.9,6.89A.75.75,0,1,1,9.42,14l5.13-5.12a1,1,0,0,0-1.42-1.42L8,12.6a2.74,2.74,0,0,0,0,3.89,2.82,2.82,0,0,0,3.89,0l6.89-6.9a4.5,4.5,0,0,0-6.36-6.36l-8,8A6.25,6.25,0,0,0,13.31,20l6.19-6.18a1,1,0,1,0-1.42-1.42Z"/></svg></button>`,
  }),
};

export const IconTransparent: Story = {
  render: (args) => ({
    props: args,
    template: `<button kn-button [rounded]="true" variant="transparent"><svg height="20" width="20" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path d="M18.08,12.42,11.9,18.61a4.25,4.25,0,0,1-6-6l8-8a2.57,2.57,0,0,1,3.54,0,2.52,2.52,0,0,1,0,3.54l-6.9,6.89A.75.75,0,1,1,9.42,14l5.13-5.12a1,1,0,0,0-1.42-1.42L8,12.6a2.74,2.74,0,0,0,0,3.89,2.82,2.82,0,0,0,3.89,0l6.89-6.9a4.5,4.5,0,0,0-6.36-6.36l-8,8A6.25,6.25,0,0,0,13.31,20l6.19-6.18a1,1,0,1,0-1.42-1.42Z"/></svg></button>`,
  }),
};
