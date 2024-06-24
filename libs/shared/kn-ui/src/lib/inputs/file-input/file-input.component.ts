import { Component, Inject, Injector, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AbstractInput } from '../../abstractions/abstract-input-component';
import { FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
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

  onFileSelect($event: any) {
    //If tus disabled => onModelChange($event)
    console.debug($event, this.fileUploaderService.fileServer);
  }
}
