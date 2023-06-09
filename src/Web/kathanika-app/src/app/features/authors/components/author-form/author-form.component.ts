import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-author-form',
  templateUrl: './author-form.component.html',
  styleUrls: ['./author-form.component.scss'],
})
export class AuthorFormComponent {
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
  }
}
