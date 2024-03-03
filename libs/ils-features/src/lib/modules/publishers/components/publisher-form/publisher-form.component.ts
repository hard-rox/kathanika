import {
  ChangeDetectionStrategy,
  Component,
  Input,
  Output,
} from '@angular/core';
import { PublisherFormInput } from '../../types/publisher-form-input';
import { PublisherFormOutput } from '../../types/publisher-form-output';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import {
  BaseFormComponent,
  FormGroupModel,
} from '../../../../abstractions/base-form-component';

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

  protected createFormGroup(): FormGroupModel<PublisherFormOutput> {
    const group = new FormGroup({
      name: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      description: new FormControl<string | null>(null, { nonNullable: false }),
      contactInformation: new FormControl<string | null>(null, {
        nonNullable: false,
      }),
    });
    return group;
  }
}
