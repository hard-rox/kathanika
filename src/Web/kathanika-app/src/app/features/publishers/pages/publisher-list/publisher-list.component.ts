import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageAlertService } from 'src/app/core/services/message-alert.service';
import { GetPublishersGQL, GetPublishersQuery, GetPublishersQueryVariables, SortEnumType } from 'src/app/graphql/generated/graphql-operations';
import { BasePaginatedListComponent } from 'src/app/shared/bases/base-paginated-list-component';

@Component({
  templateUrl: './publisher-list.component.html',
  styleUrls: ['./publisher-list.component.scss']
})
export class PublisherListComponent extends BasePaginatedListComponent<
  GetPublishersQuery,
  GetPublishersQueryVariables
>
  implements OnInit {

  protected setSearchTextQueryFilter(searchText: string): void {
    if (!searchText || searchText.length == 0) {
      this.queryVariables.filter = null;
      return;
    }

    this.queryVariables.filter = {
      or: [
        {
          name: {
            contains: searchText,
          },
        },
        {
          description: {
            contains: searchText,
          },
        }
      ],
    };
  }

  constructor(
    gql: GetPublishersGQL,
    activatedRoute: ActivatedRoute,
    router: Router,
    private alertService: MessageAlertService
  ) {
    super(gql, activatedRoute, router, {
      skip: 0,
      take: 20,
      sortBy: {
        name: SortEnumType.Asc,
      },
    });
  }

  ngOnInit(): void {
    this.init();
  }
}
