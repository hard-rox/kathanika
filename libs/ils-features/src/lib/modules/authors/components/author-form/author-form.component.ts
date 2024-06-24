import { Component, Input, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  BaseFormComponent,
  ControlsOf
} from '../../../../abstractions/base-form-component';
import { AddAuthorInput, Author, AuthorPatchInput } from '@kathanika/graphql-ts-client';
import * as tus from 'tus-js-client';
@Component({
  selector: 'kn-author-form',
  templateUrl: './author-form.component.html'
})
export class AuthorFormComponent
  extends BaseFormComponent<AddAuthorInput | AuthorPatchInput> {
  @Input()
  set author(input: Author) {
    if (input) {
      this.formGroup.patchValue({
        firstName: input.firstName,
        lastName: input.lastName,
        dateOfBirth: input.dateOfBirth,
        markedAsDeceased:
          input.dateOfDeath != null && input.dateOfDeath != undefined,
        dateOfDeath: input.dateOfDeath,
        nationality: input.nationality,
        biography: input.biography,
      });
    }
  }

  @Output()
  formSubmit = this.submitEventEmitter;

  protected override createFormGroup(): FormGroup<ControlsOf<AddAuthorInput | AuthorPatchInput>> {
    return new FormGroup<ControlsOf<AddAuthorInput | AuthorPatchInput>>({
      firstName: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      lastName: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      dateOfBirth: new FormControl<Date | null>(null, {
        nonNullable: true,
        validators: [Validators.required],
      }),
      dateOfDeath: new FormControl<Date | null>(null),
      markedAsDeceased: new FormControl<boolean>(false, { nonNullable: true }),
      nationality: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      biography: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      dpFileId: new FormControl<string | null>(null)
    });
  }

  percentage = 0;
  uploadUrl = '';
  fileUpload(eventTarget: any) {
    console.debug(eventTarget.files);
    const file: File = eventTarget.files[0];

    const upload = new tus.Upload(file, {
      endpoint: 'http://localhost:5289/fs/',
      retryDelays: [0, 3000, 5000, 10000, 20000],
      metadata: {
        filename: file.name,
        filetype: file.type,
      },
      onError: function (error) {
        console.log('Failed because: ' + error)
      },
      onProgress: (bytesUploaded, bytesTotal) => {
        this.percentage = +((bytesUploaded / bytesTotal) * 100).toFixed(2)
        console.log(bytesUploaded, bytesTotal, this.percentage + '%')
      },
      onSuccess: () => {
        console.log('Download %s from %s', (upload.file as File).name, upload.url);
        this.uploadUrl = upload.url ?? '';
        const fileId = upload.url?.replace('http://localhost:5289/fs/', '');
        console.debug(fileId);
        this.formGroup.patchValue({
          dpFileId: fileId
        });

        console.debug(this.formGroup.value);
      },
    })

    // Check if there are any previous uploads to continue.
    upload.findPreviousUploads().then(function (previousUploads) {
      // Found previous uploads so we select the first one.
      if (previousUploads.length) {
        upload.resumeFromPreviousUpload(previousUploads[0])
      }

      // Start the upload
      upload.start();
    })
  }
}
