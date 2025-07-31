import {Component} from '@angular/core';
import {BaseFormComponent, FormControlsOf} from "../../../abstractions/base-form-component";
import {CreateBibRecordInput} from "../../../graphql/generated/graphql-operations";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {KnButton, KnTextareaInput, KnTextInput} from "@kathanika/kn-ui";
import {CommonModule} from "@angular/common";

@Component({
    selector: 'app-book-record-form',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        KnTextInput,
        KnButton,
        KnTextareaInput
    ],
    standalone: true,
    templateUrl: './book-record-form.component.html'
})
export class BookRecordFormComponent extends BaseFormComponent<CreateBibRecordInput> {
    protected override createFormGroup(): FormGroup {
        return new FormGroup({
            title: new FormControl<string>('', {nonNullable: true, validators: [Validators.required]}),
            isbn: new FormControl<string | null>(null),
            author: new FormControl<string | null>(null),
            publisherName: new FormControl<string | null>(null),
            publicationDate: new FormControl<string | null>(null),
            extent: new FormControl<string | null>(null),
            language: new FormControl<string | null>(null),
            edition: new FormControl<string | null>(null),
            category: new FormControl<string | null>(null),
            description: new FormControl<string | null>(null),
            coverImageId: new FormControl<string | null>(null)
        });
    }
}
