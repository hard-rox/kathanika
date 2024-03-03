import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BasePaginatedListComponent } from '../../../../abstractions/base-paginated-list-component';
import {
  GetMembersQuery,
  GetMembersQueryVariables,
  MembershipStatus,
  GetMembersGQL,
} from '@kathanika/graphql-ts-client';

@Component({
  templateUrl: './member-list.component.html'
})
export class MemberListComponent
  extends BasePaginatedListComponent<GetMembersQuery, GetMembersQueryVariables>
  implements OnInit
{
  protected membershipStatus = MembershipStatus;

  protected setSearchTextQueryFilter(searchText: string): void {
    if (!searchText || searchText.length == 0) {
      this.queryVariables.filter = null;
      return;
    }
    this.queryVariables.filter = {
      or: [
        {
          firstName: {
            contains: searchText.split(' ')[0],
          },
        },
        {
          lastName: {
            contains: searchText.split(' ')[-1],
          },
        },
        {
          address: {
            contains: searchText,
          },
        },
        {
          contactNumber: {
            contains: searchText,
          },
        },
        {
          email: {
            contains: searchText,
          },
        },
      ],
    };
  }

  constructor(
    gql: GetMembersGQL,
    activatedRoute: ActivatedRoute,
    router: Router,
  ) {
    super(gql, activatedRoute, router, {
      skip: 0,
      take: 10,
    });
  }

  ngOnInit(): void {
    this.init();
  }
}
