import { Component, OnInit } from '@angular/core';

@Component({
  templateUrl: './author-list.component.html',
  styleUrls: ['./author-list.component.scss'],
})
export class AuthorListComponent implements OnInit {

  authors: any;
  
  constructor(
  ) { }

  ngOnInit(): void {
  }
}
