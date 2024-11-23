import {Component} from '@angular/core';
import {RouterLink, RouterLinkActive} from "@angular/router";
import {CommonModule} from "@angular/common";
import {KnButton} from "@kathanika/kn-ui";

@Component({
    selector: 'app-sidebar',
    standalone: true,
    imports: [
        CommonModule,
        RouterLink,
        RouterLinkActive,
        KnButton
    ],
    templateUrl: './sidebar.component.html'
})
export class SidebarComponent {
    selectedIndex: number | null = null;
    menus = [
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
    ];
}
