import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageAlertService } from '../../../../core/services/message-alert/message-alert.service';
import {
  GetPublicationsGQL,
  GetPublicationsQuery,
  GetPublicationsQueryVariables,
  SortEnumType,
} from '@kathanika/graphql-ts-client';
import { BasePaginatedListComponent } from '../../../../abstractions/base-paginated-list-component';

@Component({
  templateUrl: './publication-list.component.html'
})
export class PublicationListComponent
  extends BasePaginatedListComponent<
    GetPublicationsQuery,
    GetPublicationsQueryVariables
  >
  implements OnInit
{
  protected setSearchTextQueryFilter(searchText: string): void {
    if (!searchText || searchText.length == 0) {
      this.queryVariables.filter = null;
      return;
    }

    this.queryVariables.filter = {
      or: [
        {
          title: {
            contains: searchText,
          },
        },
        {
          isbn: {
            contains: searchText,
          },
        },
        {
          publisher: {
            contains: searchText,
          },
        },
        {
          language: {
            contains: searchText,
          },
        },
      ],
    };
  }

  constructor(
    gql: GetPublicationsGQL,
    activatedRoute: ActivatedRoute,
    router: Router,
    private alertService: MessageAlertService,
  ) {
    super(gql, activatedRoute, router, {
      skip: 0,
      take: 20,
      sortBy: {
        title: SortEnumType.Asc,
      },
    });
  }

  ngOnInit(): void {
    this.init();
  }
}
