import {Component} from '@angular/core';
import {BaseFormComponent, FormControlsOf} from "../../../abstractions/base-form-component";
import {BookQuickAddInput} from "../../../graphql/generated/graphql-operations";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {KnButton, KnNumberInput, KnTextareaInput, KnTextInput} from "@kathanika/kn-ui";
import {CommonModule} from "@angular/common";

@Component({
    selector: 'app-book-record-form',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        KnTextInput,
        KnButton,
        KnTextareaInput,
        KnNumberInput
    ],
    standalone: true,
    templateUrl: './book-record-form.component.html'
})
export class BookRecordFormComponent extends BaseFormComponent<BookQuickAddInput> {
    protected override createFormGroup(): FormGroup<FormControlsOf<BookQuickAddInput>> {
        return new FormGroup<FormControlsOf<BookQuickAddInput>>({
            title: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
            author: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
            numberOfCopies: new FormControl<number>(1, {nonNullable: true, validators: [Validators.min(1), Validators.max(100)]}),
            isbn: new FormControl<string | null>(null),
            publisher: new FormControl<string | null>(null),
            yearOfPublication: new FormControl<number | null>(null, {nonNullable: true, validators: [Validators.min(1000)]}),
            numberOfPages: new FormControl<number | null>(null),
            language: new FormControl<string | null>(null),
            edition: new FormControl<string | null>(null),
            note: new FormControl<string | null>(null),
            coverImageId: new FormControl<string | null>(null)
        });
    }
}
