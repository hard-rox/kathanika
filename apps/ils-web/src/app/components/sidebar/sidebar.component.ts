import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { KnButton } from '@kathanika/kn-ui';

@Component({
  selector: 'kathanika-sidebar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive, KnButton],
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
          link: 'authors',
        },
        {
          text: 'Add',
          link: 'authors/add',
        },
      ],
    },
    {
      text: 'Publication',
      icon: 'menu_book',
      link: null,
      children: [
        {
          text: 'List',
          link: 'publications',
        },
        {
          text: 'Add',
          link: 'publications/add',
        },
        {
          text: 'Record Purchase',
          link: 'publications/purchase'
        }
      ],
    },
    {
      text: 'Publishers',
      icon: 'print',
      link: null,
      children: [
        {
          text: 'List',
          link: 'publishers',
        },
        {
          text: 'Add',
          link: 'publishers/add',
        },
      ],
    },
    {
      text: 'Members',
      icon: 'person',
      link: null,
      children: [
        {
          text: 'List',
          link: 'members'
        },
        {
          text: 'Add',
          link: 'members/add'
        }
      ]
    },
  ];
}
