import { ChangeDetectionStrategy, Component, Input, Output } from '@angular/core';
import { PublisherFormInput } from '../../types/publisher-form-input';
import { PublisherFormOutput } from '../../types/publisher-form-output';
import { FormControl, Validators } from '@angular/forms';
import { BaseFormComponent } from 'src/app/shared/bases/base-form-component';

@Component({
  selector: 'kn-publisher-form',
  templateUrl: './publisher-form.component.html',
  styleUrls: ['./publisher-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PublisherFormComponent extends BaseFormComponent<PublisherFormOutput> {

  @Input('publisher')
  set publisher(input: PublisherFormInput | null | undefined) {
    if (input) {
      this.formGroup.patchValue(input);
    }
  }

  @Output('onSubmit')
  onSubmit = this.submitEventEmitter;

  constructor() {
    super();
    this.addControlsToForm();
  }

  private addControlsToForm() {
    this.formGroup.addControl('name', new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }));
    this.formGroup.addControl('description', new FormControl<string | null>(null, { nonNullable: false }));
    this.formGroup.addControl('contactInformation', new FormControl<string | null>(null, { nonNullable: false }));
  }
}
