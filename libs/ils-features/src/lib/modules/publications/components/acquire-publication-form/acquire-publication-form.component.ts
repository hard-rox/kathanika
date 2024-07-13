import { Component, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AcquirePublicationInput, AcquisitionMethod, PublicationType } from '@kathanika/graphql-ts-client';
import { BaseFormComponent, ControlsOf } from '../../../../abstractions/base-form-component';

@Component({
  selector: 'kn-acquire-publication-form',
  templateUrl: './acquire-publication-form.component.html'
})
export class AcquirePublicationFormComponent
  extends BaseFormComponent<AcquirePublicationInput>
  implements OnInit {

  @Output()
  formSubmit = this.submitEventEmitter;

  protected publicationTypes: string[] = Object.values(PublicationType);
  protected acquisitionMethod = AcquisitionMethod;
  protected acquisitionMethods: string[] = Object.values(AcquisitionMethod);

  private onAcquisitionMethodChange(method: AcquisitionMethod) {
    if (method === AcquisitionMethod.Purchase) {
      this.formGroup.removeControl('patron');
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      this.formGroup.addControl('unitPrice', new FormControl<number | any>(null, { nonNullable: true, validators: [Validators.required] }));
      this.formGroup.addControl('vendor', new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }));
      return;
    }
    this.formGroup.removeControl('unitPrice');
    this.formGroup.removeControl('vendor');
    this.formGroup.addControl('patron', new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }));
  }

  ngOnInit(): void {
    this.formGroup.controls.acquisitionMethod
      .valueChanges.subscribe({
        next: (changedValue) => {
          this.onAcquisitionMethodChange(changedValue);
        }
      })
  }

  protected override createFormGroup(): FormGroup<ControlsOf<AcquirePublicationInput>> {
    return new FormGroup<ControlsOf<AcquirePublicationInput>>({
      title: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      publicationType: new FormControl<PublicationType | any>(null, {
        nonNullable: true,
        validators: [Validators.required],
      }),
      publishedDate: new FormControl<Date | null>(null, {
        nonNullable: true,
        validators: [Validators.required],
      }),
      publisherId: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      isbn: new FormControl<string | null>(null),
      edition: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      language: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      description: new FormControl<string | null>(null, { nonNullable: false }),
      authorIds: new FormControl<string[]>([], { nonNullable: true }),
      callNumber: new FormControl<string>('', {
        nonNullable: true,
        validators: [Validators.required],
      }),
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      coverImageFileId: new FormControl<string | any>(null, {nonNullable: true, validators: [Validators.required]}),
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      quantity: new FormControl<number | any>(null, { nonNullable: true, validators: [Validators.required, Validators.min(1)] }),
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      acquisitionMethod: new FormControl<AcquisitionMethod | any>(null, {
        nonNullable: true,
        validators: [Validators.required],
      })
    });
  }
}
