import {
  ChangeDetectionStrategy,
  Component,
  Input,
  Output,
} from '@angular/core';
import {
  BaseFormComponent,
  ControlsOf
} from '../../../../abstractions/base-form-component';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AddPublisherInput, Publisher, PublisherPatchInput } from '@kathanika/graphql-ts-client';

@Component({
  selector: 'kn-publisher-form',
  templateUrl: './publisher-form.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PublisherFormComponent extends BaseFormComponent<AddPublisherInput | PublisherPatchInput> {
  @Input()
  set publisher(input: Publisher) {
    if (input) {
      this.formGroup.patchValue(input);
    }
  }

  @Output()
  formSubmit = this.submitEventEmitter;

  protected override createFormGroup(): FormGroup<ControlsOf<AddPublisherInput | PublisherPatchInput>> {
    return new FormGroup<ControlsOf<AddPublisherInput | PublisherPatchInput>>({
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
