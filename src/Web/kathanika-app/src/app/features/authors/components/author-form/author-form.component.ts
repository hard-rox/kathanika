import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthorFormInput } from '../../types/author-form-input';
import { AuthorFormOutput } from '../../types/author-form-output';

@Component({
  selector: 'app-author-form',
  templateUrl: './author-form.component.html',
  styleUrls: ['./author-form.component.scss'],
})
export class AuthorFormComponent {
  @Input('author')
  set author(input: AuthorFormInput | null | undefined) {
    if (input) {
      this.isUpdate = true;
      this.authorFromGroup.patchValue({
        firstName: input.firstName,
        lastName: input.lastName,
        dateOfBirth: input.dateOfBirth,
        dateOfDeath: input.dateOfDeath,
        nationality: input.nationality,
        biography: input.biography,
      });
    }
  }

  @Output('onSubmit')
  onSubmit = new EventEmitter<AuthorFormOutput>();

  constructor(private formBuilder: FormBuilder) {}

  isUpdate: boolean = false;
  authorFromGroup: FormGroup = this.formBuilder.group({
    firstName: [null, [Validators.required]],
    lastName: [null, [Validators.required]],
    dateOfBirth: [null, [Validators.required]],
    dateOfDeath: [],
    nationality: [null, [Validators.required]],
    biography: [null, [Validators.required]],
  });

  submitForm() {
    if (!this.authorFromGroup.valid) {
      this.authorFromGroup.markAllAsTouched();
      return;
    }

    this.onSubmit.emit(this.authorFromGroup.value);
  }

  resetForm() {
    this.authorFromGroup.reset();
  }
}
