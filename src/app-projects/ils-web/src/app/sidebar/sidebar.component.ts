import {Component} from '@angular/core';
import {RouterLink, RouterLinkActive} from "@angular/router";
import {CommonModule} from "@angular/common";
import {KnButton} from "@kathanika/kn-ui";

@Component({
    selector: 'app-sidebar',
    imports: [
        CommonModule,
        RouterLink,
        RouterLinkActive,
        KnButton
    ],
    standalone: true,
    templateUrl: './sidebar.component.html'
})
export class SidebarComponent {
    selectedIndex: number | null = null;
    menus = [
        {
            text: 'Cataloging',
            icon: 'data_table',
            link: null,
            children: [
                {
                    text: 'Bibliographic Records',
                    link: 'cataloging',
                }
            ],
        },
        {
            text: 'Vendors',
            icon: 'two_pager_store',
            link: null,
            children: [
                {
                    text: 'List',
                    link: 'vendors',
                },
                {
                    text: 'Add',
                    link: 'vendors/add',
                },
            ],
        },
        {
            text: 'Patrons',
            icon: 'person',
            link: null,
            children: [
                {
                    text: 'List',
                    link: 'patrons'
                },
                {
                    text: 'Add',
                    link: 'patrons/add'
                }
            ]
        },
        {
            text: 'Acquisitions',
            icon: 'library_books',
            link: null,
            children: [
                {
                    text: 'Purchase Orders',
                    link: 'purchase-orders',
                },
            ]
        }
    ];
}
