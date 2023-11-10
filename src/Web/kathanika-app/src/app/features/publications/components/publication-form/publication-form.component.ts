import { ChangeDetectionStrategy, Component, Input, Output } from '@angular/core';
import { PublicationFormInput } from '../../types/publication-form-input';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { PublicationFormOutput } from '../../types/publication-form-output';
import { PublicationType, SearchAuthorsGQL, SearchAuthorsQuery, SearchAuthorsQueryVariables } from 'src/app/graphql/generated/graphql-operations';
import { BaseFormComponent, FormGroupModel } from 'src/app/shared/bases/base-form-component';
import { KnValidators } from 'src/app/shared/validators/kn-validators';
import { QueryRef } from 'apollo-angular';
import { PublicationAuthor } from '../../types/publication-author';

@Component({
  selector: 'kn-publication-form',
  templateUrl: './publication-form.component.html',
  styleUrls: ['./publication-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class PublicationFormComponent
  extends BaseFormComponent<PublicationFormOutput> {

  @Input()
  set publication(input: PublicationFormInput | null | undefined) {
    if (input) {
      this.selectedAuthors = input.authors;
      this.formGroup.patchValue({
        ...input,
        authorIds: input.authors.map(x => x.id),
        copiesPurchased: input.copiesAvailable
      });
    }
  }

  @Output()
  formSubmit = this.submitEventEmitter;

  protected publicationTypes: string[] = Object.values(PublicationType);
  protected authorSearchQueryRef: QueryRef<SearchAuthorsQuery, SearchAuthorsQueryVariables>;
  protected selectedAuthors: PublicationAuthor[] = [];

  constructor(authorsGql: SearchAuthorsGQL) {
    super();
    this.authorSearchQueryRef = authorsGql.watch({ filterText: '' });
  }

  protected createFormGroup(): FormGroupModel<PublicationFormOutput> {
    const group: FormGroupModel<PublicationFormOutput> = new FormGroup({
      title: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      publicationType: new FormControl<PublicationType | any>(null, { nonNullable: true, validators: [Validators.required] }),
      publishedDate: new FormControl<Date | null>(null, { nonNullable: true, validators: [Validators.required] }),
      publisher: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      isbn: new FormControl<string | null>(null, { nonNullable: false }),
      edition: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      language: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      description: new FormControl<string | null>(null, { nonNullable: false }),
      authorIds: new FormControl<string[]>([], { nonNullable: true }),
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
      buyingPrice: new FormControl<number | any>(null, { nonNullable: true, validators: [Validators.required, Validators.min(0)] }),
      callNumber: new FormControl<string>('', { nonNullable: true, validators: [Validators.required] }),
      // eslint-disable-next-line @typescript-eslint/no-explicit-any
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

  protected addAuthor(author: PublicationAuthor) {
    const index = this.selectedAuthors.findIndex(x => x.id == author.id);
    if (index >= 0) return;
    this.selectedAuthors.push(author);
    this.formGroup.controls.authorIds.value?.push(author.id);
  }

  protected removeAuthor(authorId: string) {
    const selectedAuthorIndex = this.selectedAuthors.findIndex(x => x.id == authorId);
    const formValueIndex = this.formGroup.controls.authorIds.value?.findIndex(x => x == authorId);
    if (selectedAuthorIndex < 0 || (formValueIndex && formValueIndex < 0)) return;

    this.selectedAuthors.splice(selectedAuthorIndex, 1);
    this.formGroup.controls.authorIds.value?.splice(formValueIndex ?? -1, 1);
  }
}
