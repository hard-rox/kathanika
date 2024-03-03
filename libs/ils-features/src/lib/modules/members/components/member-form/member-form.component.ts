import { ChangeDetectionStrategy, Component, Input, Output } from '@angular/core';
import { BaseFormComponent, FormGroupModel } from "../../../../abstractions/base-form-component";
import { MemberFormOutput } from '../../types/member-form-output';
import { MemberFormInput } from '../../types/member-form-input';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'kn-member-form',
  templateUrl: './member-form.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MemberFormComponent extends BaseFormComponent<MemberFormOutput>
{
  @Input()
  set member(input: MemberFormInput) {
    if (input) {
      this.formGroup.patchValue({
        firstName: input.firstName,
        lastName: input.lastName,
        dateOfBirth: input.dateOfBirth,
        contactNumber: input.contactNumber,
        email: input.email,
        address: input.address
      });
    }
  }

  @Output()
  formSubmit = this.submitEventEmitter;

  protected createFormGroup(): FormGroupModel<MemberFormOutput> {
    const group: FormGroupModel<MemberFormOutput> = new FormGroup({
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
      contactNumber: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      email: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      address: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      })
    });
    return group;
  }
}
