import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  GetMemberQuery,
  GetMemberQueryVariables,
  GetMemberGQL,
} from '@kathanika/graphql-ts-client';
import { BaseQueryComponent } from '../../../../abstractions/base-query-component';

@Component({
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.scss'],
})
export class MemberDetailsComponent
  extends BaseQueryComponent<GetMemberQuery, GetMemberQueryVariables>
  implements OnInit
{
  constructor(
    gql: GetMemberGQL,
    private activatedRoute: ActivatedRoute,
  ) {
    super(gql);
  }

  ngOnInit(): void {
    const memberId = this.activatedRoute.snapshot.params['id'];
    if (memberId && memberId.length > 0) {
      this.queryVariables = {
        id: memberId,
      };
      this.queryRef.refetch(this.queryVariables);
    }
  }
}
