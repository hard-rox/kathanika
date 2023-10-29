import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  DeleteAuthorGQL,
  GetAuthorsGQL,
  GetAuthorsQuery,
  GetAuthorsQueryVariables,
  SortEnumType,
} from 'src/app/graphql/generated/graphql-operations';
import { MessageAlertService } from 'src/app/core/services/message-alert.service';
import { BasePaginatedListComponent } from 'src/app/shared/bases/base-paginated-list-component';

@Component({
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.scss'],
})
export class AuthorListComponent
  extends BasePaginatedListComponent<GetAuthorsQuery, GetAuthorsQueryVariables>
  implements OnInit {

  protected setSearchTextQueryFilter(searchText: string): void {
    if (!searchText || searchText.length == 0) {
      this.queryVariables.filter = null;
      return;
    }

    this.queryVariables.filter = {
      or: [
        {
          firstName: {
            contains: searchText,
          },
        },
        {
          lastName: {
            contains: searchText,
          },
        },
        {
          nationality: {
            contains: searchText,
          },
        },
      ],
    };
  }

  constructor(
    gql: GetAuthorsGQL,
    private deleteAuthorGql: DeleteAuthorGQL,
    activatedRoute: ActivatedRoute,
    router: Router,
    private alertService: MessageAlertService
  ) {
    super(gql, activatedRoute, router, {
      skip: 0,
      take: 10,
      sortBy: {
        firstName: SortEnumType.Asc,
      },
    });
  }

  ngOnInit(): void {
    this.init();
  }

  deleteAuthor(id: string, name: string) {
    this.alertService
      .showConfirmation(
        'warning',
        `Are you sure deleting author ${name}? This action is irreversible.`,
        'Confirm deleting author?',
        'Yes, Delete'
      )
      .subscribe({
        next: (isConfirmed) => {
          if (isConfirmed) {
            this.deleteAuthorGql.mutate({
              id: id
            }).subscribe({
              next: (result) => {
                if (result.data?.deleteAuthor.message) {
                  this.alertService.showPopup(
                    'success',
                    result.data?.deleteAuthor.message,
                    'Deleted successfully');
                } else {
                  this.alertService.showPopup(
                    'error',
                    result.data?.deleteAuthor.errors?.map(x => x.message).join(',') ?? 'Deletion failed',
                    'Deletion failed'
                  );
                }
              }
            })
          }
        },
      });
  }
}
