import { ChangeDetectionStrategy, Component, Input, Output } from '@angular/core';
import { PublisherFormInput } from '../../types/publisher-form-input';
import { PublisherFormOutput } from '../../types/publisher-form-output';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BaseFormComponent, FormGroupModel } from 'src/app/shared/bases/base-form-component';

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

  protected createFormGroup(): FormGroupModel<PublisherFormOutput> {
    const group = new FormGroup({
      name: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      description: new FormControl<string | null>(null, { nonNullable: false }),
      contactInformation: new FormControl<string | null>(null, { nonNullable: false }),
    });
    return group;
  }
}
