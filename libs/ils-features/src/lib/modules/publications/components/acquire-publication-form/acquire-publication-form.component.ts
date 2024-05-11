import { Component, Input, OnInit, Output } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { AcquirePublicationInput, AcquisitionMethod, Publication, PublicationAuthor, PublicationType, SearchAuthorsGQL, SearchAuthorsQuery, SearchAuthorsQueryVariables } from '@kathanika/graphql-ts-client';
import { QueryRef } from 'apollo-angular';
import { BaseFormComponent, ControlsOf } from '../../../../abstractions/base-form-component';

@Component({
  selector: 'kn-acquire-publication-form',
  templateUrl: './acquire-publication-form.component.html'
})
export class AcquirePublicationFormComponent
  extends BaseFormComponent<AcquirePublicationInput>
  implements OnInit {
  @Input()
  set publication(input: Publication) {
    if (input) {
      this.selectedAuthors = input.authors;
      this.formGroup.patchValue({
        ...input,
        authorIds: input.authors.map((x) => x.id)
      });
    }
  }

  @Output()
  formSubmit = this.submitEventEmitter;

  protected publicationTypes: string[] = Object.values(PublicationType);
  protected acquisitionMethod = AcquisitionMethod;
  protected acquisitionMethods: string[] = Object.values(AcquisitionMethod);
  protected authorSearchQueryRef: QueryRef<
    SearchAuthorsQuery,
    SearchAuthorsQueryVariables
  >;
  protected selectedAuthors: PublicationAuthor[] = [];

  constructor(authorsGql: SearchAuthorsGQL) {
    super();
    this.authorSearchQueryRef = authorsGql.watch({ filterText: '' });
  }

  private someMethod(method: AcquisitionMethod) {
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
          this.someMethod(changedValue);
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
      publisher: new FormControl<string>('', {
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
      quantity: new FormControl<number | any>(0, { nonNullable: true, validators: [Validators.required] }),
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      acquisitionMethod: new FormControl<AcquisitionMethod | any>(null, {
        nonNullable: true,
        validators: [Validators.required],
      })
    });
  }

  protected filter(filterText: string) {
    const queryVariable: SearchAuthorsQueryVariables = {
      filterText: filterText,
    };
    this.authorSearchQueryRef.refetch(queryVariable);
  }

  protected addAuthor(author: PublicationAuthor) {
    const index = this.selectedAuthors.findIndex((x) => x.id == author.id);
    if (index >= 0) return;
    this.selectedAuthors.push(author);
    this.formGroup.controls.authorIds.value?.push(author.id);
  }

  protected removeAuthor(authorId: string) {
    const selectedAuthorIndex = this.selectedAuthors.findIndex(
      (x) => x.id == authorId,
    );
    const formValueIndex = this.formGroup.controls.authorIds.value?.findIndex(
      (x) => x == authorId,
    );
    if (selectedAuthorIndex < 0 || (formValueIndex && formValueIndex < 0))
      return;

    this.selectedAuthors.splice(selectedAuthorIndex, 1);
    this.formGroup.controls.authorIds.value?.splice(formValueIndex ?? -1, 1);
  }
}
