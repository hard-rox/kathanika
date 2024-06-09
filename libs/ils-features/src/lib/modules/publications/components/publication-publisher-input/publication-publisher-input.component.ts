import { Component, Inject, Injector, Input } from '@angular/core';
import { NG_VALUE_ACCESSOR } from '@angular/forms';
import { PublicationPublisher, Publisher, SearchPublishersGQL, SearchPublishersQuery, SearchPublishersQueryVariables } from '@kathanika/graphql-ts-client';
import { AbstractInput } from '@kathanika/kn-ui';
import { QueryRef } from 'apollo-angular';

@Component({
  selector: 'kn-publication-publisher-input',
  templateUrl: './publication-publisher-input.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: PublicationPublisherInputComponent,
    },
  ],
})
export class PublicationPublisherInputComponent extends AbstractInput<string> {
  protected publisherSearchQueryRef: QueryRef<
    SearchPublishersQuery,
    SearchPublishersQueryVariables
  >;
  @Input()
  set currentPublisher(input: PublicationPublisher | null) {
    if (input) {
      this.selectedPublisher = input;
      this.value = input.id;
    }
  }

  protected selectedPublisher: { id: string, name: string } | null = null;
  protected getPublisherName = (publisher: Publisher) => publisher.name;

  constructor(@Inject(Injector) injector: Injector, publisherGql: SearchPublishersGQL) {
    super(injector);
    this.value = null;
    this.publisherSearchQueryRef = publisherGql.watch({ filterText: '' });
  }

  protected filter(filterText: string) {
    const queryVariable: SearchPublishersQueryVariables = {
      filterText: filterText,
    };
    this.publisherSearchQueryRef.refetch(queryVariable);
  }

  protected selectPublisher(publisher: Publisher) {
    this.selectedPublisher = publisher;
    this.value = publisher.id;
  }

  protected removePublisher() {
    this.selectedPublisher = null;
    this.value = null;
  }
}
