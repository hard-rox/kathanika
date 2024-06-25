import { Component, Inject, Injector, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractInput } from '../../abstractions/abstract-input-component';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import * as tus from 'tus-js-client';
import { FileUploaderService } from '../../services/file-uploader/file-uploader.service';

@Component({
  selector: 'kn-file-input',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './file-input.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: KnFileInput,
    },
  ],
})
export class KnFileInput extends AbstractInput<string | string[]> {
  @Input()
  multiple = false;

  constructor(
    @Inject(Injector) injector: Injector,
    private fileUploaderService: FileUploaderService
  ) {
    super(injector);
  }

  protected fileUploads: {
    file: File,
    uploadPercentage: number,
    uploadCompleted: boolean,
    fileId: string | null
  }[] = [];

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  onFileSelect($event: any) {
    console.debug($event.target.files);
    //If tus disabled => onModelChange($event)

    if ($event.target.file <= 0) {
      this.fileUploads = [];
      return;
    };

    const file: File = $event.target.files[0];
    this.fileUploads.push({
      file: file,
      uploadPercentage: 0,
      uploadCompleted: false,
      fileId: null
    });

    this.fileUploads.forEach(x => {
      const upload = new tus.Upload(file, {
        endpoint: this.fileUploaderService.fileServer,
        retryDelays: [0, 3000, 5000, 10000, 20000],
        metadata: {
          filename: file.name,
          filetype: file.type,
        },
        onError: (error) => {
          console.log('Failed because: ' + error);
        },
        onProgress: (bytesUploaded, bytesTotal) => {
          x.uploadPercentage = +((bytesUploaded / bytesTotal) * 100).toFixed(2);
        },
        onSuccess: () => {
          console.log('Download %s from %s', (upload.file as File).name, upload.url);
          const fileId = upload.url?.split('/').pop();
          if (fileId) {
            this.onModelChange(fileId);
          }
          console.debug(fileId);
        },
      });

      upload.start();
    });
  }
}
