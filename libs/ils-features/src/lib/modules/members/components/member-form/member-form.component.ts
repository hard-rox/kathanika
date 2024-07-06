import { ChangeDetectionStrategy, Component, Input, Output } from '@angular/core';
import { BaseFormComponent, ControlsOf } from "../../../../abstractions/base-form-component";
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CreateMemberInput, Member, MemberPatchInput } from '@kathanika/graphql-ts-client';

@Component({
  selector: 'kn-member-form',
  templateUrl: './member-form.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MemberFormComponent extends BaseFormComponent<CreateMemberInput | MemberPatchInput>
{
  @Input()
  set member(input: Member | null) {
    if (input) {
      this.formGroup.patchValue(input);
    }
  }

  @Output()
  formSubmit = this.submitEventEmitter;

  protected override createFormGroup(): FormGroup<ControlsOf<CreateMemberInput | MemberPatchInput>> {
    return new FormGroup<ControlsOf<CreateMemberInput | MemberPatchInput>>({
      firstName: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      lastName: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      photoFileId: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required]
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
  }
}
