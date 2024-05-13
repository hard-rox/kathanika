import { Component, Inject, Injector, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { PublicationAuthor, SearchAuthorsGQL, SearchAuthorsQuery, SearchAuthorsQueryVariables } from '@kathanika/graphql-ts-client';
import { AbstractInputComponent } from '@kathanika/kn-ui';
import { QueryRef } from 'apollo-angular';

@Component({
  selector: 'kn-publication-authors-input',
  templateUrl: './publication-authors-input.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: PublicationAuthorsInputComponent,
    },
  ],
})
export class PublicationAuthorsInputComponent extends AbstractInputComponent<string[]> {
  protected authorSearchQueryRef: QueryRef<
    SearchAuthorsQuery,
    SearchAuthorsQueryVariables
  >;
  @Input()
  set currentAuthors(input: PublicationAuthor[] | null) {
    if (input) this.selectedAuthors = input;
  }

  protected selectedAuthors: PublicationAuthor[] = [];

  constructor(@Inject(Injector) injector: Injector, authorGql: SearchAuthorsGQL) {
    super(injector);
    this.value = [];
    this.authorSearchQueryRef = authorGql.watch({ filterText: '' });
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
    this.selectedAuthors = [...this.selectedAuthors, author];
    this.value?.push(author.id);
  }

  protected removeAuthor(authorId: string) {
    const selectedAuthorIndex = this.selectedAuthors.findIndex(
      (x) => x.id == authorId,
    );
    const formValueIndex = this.value?.findIndex(
      (x) => x == authorId,
    );
    if (selectedAuthorIndex < 0 || (formValueIndex && formValueIndex < 0))
      return;

    this.selectedAuthors = this.selectedAuthors.filter(x => x.id !== authorId);
    this.value?.splice(formValueIndex ?? -1, 1);
  }
}
