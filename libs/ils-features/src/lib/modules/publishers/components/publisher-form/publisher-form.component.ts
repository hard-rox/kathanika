import {
  ChangeDetectionStrategy,
  Component,
  Input,
  Output,
} from '@angular/core';
import { PublisherFormInput } from '../../types/publisher-form-input';
import { PublisherFormOutput } from '../../types/publisher-form-output';
import {
  BaseFormComponent,
  ControlsOf
} from '../../../../abstractions/base-form-component';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'kn-publisher-form',
  templateUrl: './publisher-form.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PublisherFormComponent extends BaseFormComponent<PublisherFormOutput> {
  @Input()
  set publisher(input: PublisherFormInput) {
    if (input) {
      this.formGroup.patchValue(input);
    }
  }

  @Output()
  formSubmit = this.submitEventEmitter;

  protected override createFormGroup(): FormGroup<ControlsOf<PublisherFormOutput>> {
    return new FormGroup({
      name: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      description: new FormControl<string | null>(null, { nonNullable: false }),
      contactInformation: new FormControl<string | null>(null, {
        nonNullable: false,
      }),
    });
  }
}
