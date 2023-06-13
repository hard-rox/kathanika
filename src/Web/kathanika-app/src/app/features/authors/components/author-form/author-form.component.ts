import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-author-form',
  templateUrl: './author-form.component.html',
  styleUrls: ['./author-form.component.scss'],
})
export class AuthorFormComponent {
  @Input('author')
  author:
    | {
        firstName: string;
        lastName: string;
        dateOfBirth: any;
        dateOfDeath?: any;
        nationality: string;
        biography: string;
      }
    | null
    | undefined;

  @Output('onSubmit')
  onSubmit = new EventEmitter<{
    firstName: string;
    lastName: string;
    dateOfBirth: Date;
    dateOfDeath: Date | null;
    nationality: string;
    biography: string;
  }>();

  constructor(private formBuilder: FormBuilder) {}

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
