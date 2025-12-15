import type {Meta, StoryObj} from "@storybook/angular";
import {KnFileInput} from "./file-input.component";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';

const meta: Meta<KnFileInput> = {
    component: KnFileInput,
    title: 'Inputs/File Input',
    tags: ['autodocs'],
    argTypes: {
        multiple: {
            control: 'boolean',
            description: 'Whether multiple files can be selected',
            table: {
                type: {summary: 'boolean'},
                defaultValue: {summary: 'false'},
            },
        },
        accept: {
            control: 'text',
            description: 'Accepted file types (e.g., "image/*", ".pdf,.doc")',
            table: {
                type: {summary: 'string'},
                defaultValue: {summary: "''"},
            },
        },
    },
};
export default meta;
type Story = StoryObj<KnFileInput>;

/**
 * Basic file input
 */
export const Default: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                file: new FormControl(''),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-file-input 
                    formControlName="file"
                    label="Upload File"
                    helpText="Select a file to upload">
                </kn-file-input>
            </form>
        `,
    }),
};

/**
 * Image file input
 */
export const ImageOnly: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                image: new FormControl(''),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-file-input 
                    formControlName="image"
                    label="Upload Image"
                    [accept]="'image/*'"
                    helpText="Only image files allowed">
                </kn-file-input>
            </form>
        `,
    }),
};

/**
 * Multiple files input
 */
export const Multiple: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                documents: new FormControl(''),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-file-input 
                    formControlName="documents"
                    label="Upload Documents"
                    [multiple]="true"
                    [accept]="'.pdf,.doc,.docx'"
                    helpText="Select multiple PDF or Word documents">
                </kn-file-input>
            </form>
        `,
    }),
};

/**
 * Required file input
 */
export const Required: Story = {
    render: (args) => ({
        props: {
            form: new FormGroup({
                resume: new FormControl('', Validators.required),
            }),
        },
        moduleMetadata: {
            imports: [ReactiveFormsModule],
        },
        template: `
            <form [formGroup]="form">
                <kn-file-input 
                    formControlName="resume"
                    label="Resume *"
                    [accept]="'.pdf,.doc,.docx'"
                    helpText="PDF or Word document required">
                </kn-file-input>
                <div class="mt-2 text-sm text-gray-600">
                    File ID: {{ form.get('resume')?.value || 'None uploaded' }}
                </div>
            </form>
        `,
    }),
};
