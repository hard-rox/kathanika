import {ComponentFixture, TestBed} from '@angular/core/testing';
import {BookRecordFormComponent} from './book-record-form.component';
import {ReactiveFormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {KnTextInput, KnButton} from '@kathanika/kn-ui';
import {ApolloTestingModule} from 'apollo-angular/testing';
import {CreateBibRecordGQL, CreateBibRecordInput} from '../../../graphql/generated/graphql-operations';
import {Component} from '@angular/core';

// Create a test host component to receive output from BookRecordFormComponent
@Component({
    template: `
        <app-book-record-form (formSubmit)="onFormSubmit($event)"></app-book-record-form>`,
    standalone: false,
})
class TestHostComponent {
    submittedData: CreateBibRecordInput | null = null;

    onFormSubmit(data: CreateBibRecordInput): void {
        this.submittedData = data;
    }
}

describe('BookRecordFormComponent', () => {
    let hostComponent: TestHostComponent;
    let hostFixture: ComponentFixture<TestHostComponent>;
    let component: BookRecordFormComponent;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                CommonModule,
                ReactiveFormsModule,
                ApolloTestingModule,
                BookRecordFormComponent,
                KnTextInput,
                KnButton
            ],
            declarations: [TestHostComponent],
            providers: [
                {
                    provide: CreateBibRecordGQL,
                    useValue: {
                        mutate: jest.fn()
                    }
                }
            ]
        }).compileComponents();

        hostFixture = TestBed.createComponent(TestHostComponent);
        hostComponent = hostFixture.componentInstance;
        component = hostFixture.debugElement.children[0].componentInstance;
        hostFixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should initialize with empty form', () => {
        expect(component['formGroup'].get('title')?.value).toBe('');
        expect(component['formGroup'].get('isbn')?.value).toBeNull();
        expect(component['formGroup'].get('author')?.value).toBeNull();
        expect(component['formGroup'].get('publisherName')?.value).toBeNull();
        expect(component['formGroup'].get('publicationDate')?.value).toBeNull();
        expect(component['formGroup'].get('extent')?.value).toBeNull();
        expect(component['formGroup'].get('dimensions')?.value).toBeNull();
    });

    it('should have title field as required', () => {
        const titleControl = component['formGroup'].get('title');
        expect(titleControl?.valid).toBeFalsy();
        expect(titleControl?.hasError('required')).toBeTruthy();

        titleControl?.setValue('Test Book');
        expect(titleControl?.valid).toBeTruthy();
        expect(titleControl?.hasError('required')).toBeFalsy();
    });

    it('should not emit form values when form is invalid', () => {
        const submitSpy = jest.spyOn(component.formSubmit, 'emit');

        // Try to submit with an invalid form
        component.submitForm();
        expect(submitSpy).not.toHaveBeenCalled();

        // Form should be marked as touched
        expect(component['formGroup'].get('title')?.touched).toBeTruthy();
    });

    it('should emit form values when form is valid', () => {
        // Fill out required fields to make the form valid
        component['formGroup'].get('title')?.setValue('Test Book Title');

        // Optional fields
        component['formGroup'].get('isbn')?.setValue('9781234567890');
        component['formGroup'].get('author')?.setValue('Smith, John');

        // Submit form
        component.submitForm();

        // Check if the host component received the form data
        expect(hostComponent.submittedData).toEqual({
            title: 'Test Book Title',
            isbn: '9781234567890',
            author: 'Smith, John',
            publisherName: null,
            publicationDate: null,
            extent: null,
            dimensions: null,
            coverImageId: null
        });
    });

    it('should reset form correctly', () => {
        // Set some values
        component['formGroup'].get('title')?.setValue('Test Book');
        component['formGroup'].get('isbn')?.setValue('9781234567890');

        // Reset form
        component.resetForm();

        // Check that values are reset
        expect(component['formGroup'].get('title')?.value).toBe('');
        expect(component['formGroup'].get('isbn')?.value).toBeNull();
        expect(component['formGroup'].pristine).toBeTruthy();
    });

    it('should display form with all required fields', () => {
        const formElement = hostFixture.nativeElement.querySelector('form');
        expect(formElement).toBeTruthy();

        // Check for input fields
        const inputLabels = hostFixture.nativeElement.querySelectorAll('kn-text-input');
        expect(inputLabels.length).toBe(7); // Total number of fields

        // Check for `submit` button
        const submitButton = hostFixture.nativeElement.querySelector('input[type="submit"]');
        expect(submitButton).toBeTruthy();
        expect(submitButton.value).toBe('Save');
    });

    it('should display MARC21 help information', () => {
        const helpSection = hostFixture.nativeElement.querySelector('.bg-gray-50');
        expect(helpSection).toBeTruthy();

        // Check for MARC21 header
        const helpHeader = helpSection.querySelector('h3');
        expect(helpHeader.textContent).toContain('MARC21 Bibliographic Record Structure');

        // Check for list items with field descriptions
        const listItems = helpSection.querySelectorAll('li');
        expect(listItems.length).toBe(7); // Should have 7 field descriptions

        // Check specific field descriptions
        expect(listItems[0].textContent).toContain('Title (245$a)');
        expect(listItems[1].textContent).toContain('ISBN (020$a)');
    });
});