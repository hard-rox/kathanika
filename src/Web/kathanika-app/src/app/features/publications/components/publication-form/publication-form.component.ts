import { ChangeDetectionStrategy, Component, Input, OnInit, Output } from '@angular/core';
import { PublicationFormInput } from '../../types/publication-form-input';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { PublicationFormOutput } from '../../types/publication-form-output';
import { GetAuthorsGQL, GetAuthorsQuery, GetAuthorsQueryVariables, PublicationType, SearchAuthorsGQL, SearchAuthorsQuery, SearchAuthorsQueryVariables } from 'src/app/graphql/generated/graphql-operations';
import { BaseFormComponent, FormGroupModel } from 'src/app/shared/bases/base-form-component';
import { KnValidators } from 'src/app/shared/validators/kn-validators';
import { QueryRef } from 'apollo-angular';

@Component({
  selector: 'kn-publication-form',
  templateUrl: './publication-form.component.html',
  styleUrls: ['./publication-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PublicationFormComponent
  extends BaseFormComponent<PublicationFormOutput> {

  @Input('publication')
  set publication(input: PublicationFormInput | null | undefined) {
    if (input) {
      this.formGroup.patchValue({
        ...input,
        authorIds: input.authors,
        copiesPurchased: input.copiesAvailable
      });
    }
  }

  @Output('onSubmit')
  onSubmit = this.submitEventEmitter;

  protected publicationTypes: string[] = Object.values(PublicationType);
  protected authorSearchQueryRef: QueryRef<SearchAuthorsQuery, SearchAuthorsQueryVariables>;

  constructor(authorsGql: SearchAuthorsGQL) {
    super();
    this.authorSearchQueryRef = authorsGql.watch({ filterText: '' });
  }

  protected createFormGroup(): FormGroupModel<PublicationFormOutput> {
    const group: FormGroupModel<PublicationFormOutput> = new FormGroup({
      title: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      publicationType: new FormControl<PublicationType | any>(null, { nonNullable: true, validators: [Validators.required] }),
      publishedDate: new FormControl<Date | null>(null, { nonNullable: true, validators: [Validators.required] }),
      publisher: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      isbn: new FormControl<string | null>(null, { nonNullable: false }),
      edition: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      language: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      description: new FormControl<string | null>(null, { nonNullable: false }),
      authorIds: new FormControl<string[]>([], { nonNullable: true }),
      buyingPrice: new FormControl<number | any>(null, { nonNullable: true, validators: [Validators.required, Validators.min(0)] }),
      callNumber: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      copiesPurchased: new FormControl<number | any>(null, { nonNullable: true, validators: [Validators.required, Validators.min(1), KnValidators.integerOnly] }),
    });
    return group;
  }

  protected filter(filterText: string) {
    const queryVariable: SearchAuthorsQueryVariables = {
      filterText: filterText
    };
    this.authorSearchQueryRef.refetch(queryVariable);
  }
}
