import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { PublisherFormInput } from '../../types/publisher-form-input';
import { PublisherFormOutput } from '../../types/publisher-form-output';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'kn-publisher-form',
  templateUrl: './publisher-form.component.html',
  styleUrls: ['./publisher-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PublisherFormComponent {
  @Input('publisher')
  set publisher(input: PublisherFormInput | null | undefined) {
    if (input) {
      this.isUpdate = true;
      this.publisherFromGroup.setValue(input);
    }
  }

  @Output('onSubmit')
  onSubmit = new EventEmitter<PublisherFormOutput>();

  constructor(private formBuilder: FormBuilder) { }

  isUpdate: boolean = false;
  publisherFromGroup: FormGroup = this.formBuilder.group({
    name: [null, Validators.required],
    description: [null],
    contactInformation: [null]
  });

  submitForm() {
    if (!this.publisherFromGroup.valid) {
      this.publisherFromGroup.markAllAsTouched();
      return;
    }

    this.onSubmit.emit(this.publisherFromGroup.value);
  }

  resetForm() {
    this.publisherFromGroup.reset();
  }
}
