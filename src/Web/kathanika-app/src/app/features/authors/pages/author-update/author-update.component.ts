import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import {
  GetAuthorQuery,
  GetAuthorQueryVariables,
  GetAuthorGQL,
} from 'src/app/graphql/generated/graphql-operations';
import { BaseQueryComponent } from 'src/app/shared/bases/base-query-component';

@Component({
  templateUrl: './author-update.component.html',
  styleUrls: ['./author-update.component.scss'],
})
export class AuthorUpdateComponent
  extends BaseQueryComponent<GetAuthorQuery, GetAuthorQueryVariables>
  implements OnInit
{
  constructor(gql: GetAuthorGQL, private activatedRoute: ActivatedRoute) {
    super(gql);
  }

  authorId: string | undefined;
  author:
    | {
        firstName: string;
        lastName: string;
        dateOfBirth: any;
        dateOfDeath?: any;
        nationality: string;
        biography: string;
      }
    | null
    | undefined;

  ngOnInit(): void {
    this.authorId = this.activatedRoute.snapshot.params['id'];
    if (this.authorId && this.authorId.length > 0) {
      this.queryVariables = {
        id: this.authorId,
      };
      this.queryRef.valueChanges.subscribe({
        next: (result) => {
          console.debug(result);
          this.author = result.data.author;
        },
      });
    }
  }

  onValidFormSubmit(author: any) {}
}
