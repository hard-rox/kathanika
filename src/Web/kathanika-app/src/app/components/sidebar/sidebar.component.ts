import { Component } from '@angular/core';

@Component({
  selector: 'kn-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent {
  selectedIndex: number | null = null;
  menus = [
    {
      text: 'Authors',
      icon: 'demography',
      link: null,
      children: [
        {
          text: 'List',
          link: 'authors'
        },
        {
          text: 'Add',
          link: 'authors/add'
        }
      ]
    },
    {
      text: 'Publication',
      icon: 'menu_book',
      link: null,
      children: [
        {
          text: 'List',
          link: 'publications'
        },
        {
          text: 'Add',
          link: 'publications/add'
        }
      ]
    },
    {
      text: 'Publishers',
      icon: 'print',
      link: null,
      children: [
        {
          text: 'List',
          link: 'publishers'
        },
        {
          text: 'Add',
          link: 'publishers/add'
        }
      ]
    },
  ]
}
