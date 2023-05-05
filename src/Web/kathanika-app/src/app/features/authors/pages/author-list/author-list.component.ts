import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { GetAuthorsGQL, SortEnumType } from 'src/app/graphql/generated';

@Component({
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.scss'],
})
export class AuthorListComponent implements OnInit {
  authors: any;

  constructor(private getAuthorsGql: GetAuthorsGQL) {}

  ngOnInit(): void {
    console.debug('12345')
    this.getAuthorsGql.watch({
      skip: 0,
      take: 3,
      sortBy: {
        firstName: SortEnumType.Asc
      }
    }).valueChanges.subscribe({
      next: (res) => {
        this.authors = res;
      }
    });
  }
}
