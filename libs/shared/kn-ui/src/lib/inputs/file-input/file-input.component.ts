import { ChangeDetectorRef, Component, Inject, Injector, Input } from '@angular/core';
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
export class KnFileInput extends AbstractInput<string> {
  @Input()
  multiple = false;

  @Input()
  accept = '';

  constructor(
    @Inject(Injector) injector: Injector,
    private fileUploaderService: FileUploaderService,
    private changeDetectorRef: ChangeDetectorRef
  ) {
    super(injector);
  }

  public file: File | null = null;
  public uploadPercentage = 0;

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  onFileSelect($event: any) {
    //If tus disabled => onModelChange($event)

    this.uploadPercentage = 0;

    this.file = $event.target.files[0];
    if ($event.target.files.length <= 0 || !this.file) {
      this.file = null;
      this.onModelChange('');
      return;
    }

    const tusUpload = new tus.Upload(this.file, {
      endpoint: this.fileUploaderService.fileServer,
      retryDelays: [0, 3000, 5000, 10000, 20000],
      metadata: {
        filename: this.file.name,
        filetype: this.file.type,
      },
      onError: (error) => {
        console.error('Failed because: ' + error);
      },
      onProgress: (bytesUploaded, bytesTotal) => {
        const percentage = +((bytesUploaded / bytesTotal) * 100).toFixed(2);
        this.uploadPercentage = percentage;
        this.changeDetectorRef.detectChanges();
      },
      onSuccess: () => {
        const fileId = tusUpload.url?.split('/').pop();
        if (fileId) {
          this.onModelChange(fileId);
        }
        console.debug(fileId);
      },
    });

    tusUpload.start();
  }
}
