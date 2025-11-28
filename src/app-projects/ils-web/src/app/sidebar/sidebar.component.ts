import {Component, inject, signal} from '@angular/core';
import {Router, RouterLink, RouterLinkActive} from '@angular/router';
import {SidebarService} from './sidebar.service';

interface NavigationItem {
    label: string;
    icon: string;
    route?: string; // Optional for parent items
    children?: NavigationItem[];
    isExpanded?: boolean;
}

@Component({
    selector: 'app-sidebar',
    imports: [RouterLink, RouterLinkActive],
    templateUrl: './sidebar.component.html'
})
export class SidebarComponent {
    private sidebarService = inject(SidebarService);
    private router = inject(Router);
    private hoverTimeout: ReturnType<typeof setTimeout> | null = null;

    protected readonly isCollapsed = this.sidebarService.isCollapsed;
    protected readonly isMobileOpen = this.sidebarService.isMobileOpen;
    protected readonly hoveredItemIndex = signal<number | null>(null);

    protected readonly navigationItems = signal<NavigationItem[]>([
        {
            label: 'Dashboard',
            icon: 'dashboard',
            route: '/dashboard'
        },
        {
            label: 'Cataloging',
            icon: 'book',
            isExpanded: false,
            children: [
                {label: 'Bibliographic Records', icon: 'library_books', route: '/cataloging/bibs'},
                {label: 'Add New Record', icon: 'add', route: '/cataloging/bibs/add'}
            ]
        },
        {
            label: 'Circulation',
            icon: 'sync',
            isExpanded: false,
            children: [
                {label: 'Check Out', icon: 'output', route: '/circulation/checkout'},
                {label: 'Check In', icon: 'input', route: '/circulation/checkin'},
                {label: 'Renewals', icon: 'refresh', route: '/circulation/renewals'}
            ]
        },
        {
            label: 'Acquisition',
            icon: 'shopping_cart',
            isExpanded: false,
            children: [
                {label: 'Purchase Orders', icon: 'receipt', route: '/acquisition/purchase-orders'},
                {label: 'Vendors', icon: 'business', route: '/acquisition/vendors'}
            ]
        },
        {
            label: 'Reports',
            icon: 'analytics',
            route: '/reports'
        },
        {
            label: 'Settings',
            icon: 'settings',
            route: '/settings'
        }
    ]);

    closeMobileSidebar() {
        this.sidebarService.closeMobile();
    }

    navigateTo(route: string) {
        this.router.navigate([route]);
        // Close mobile sidebar after navigation
        if (this.isMobileOpen()) {
            this.closeMobileSidebar();
        }
        // Hide hover menu after navigation
        this.hoveredItemIndex.set(null);
    }

    toggleParentMenu(index: number) {
        const items = this.navigationItems();
        const item = items[index];
        if (item.children) {
            item.isExpanded = !item.isExpanded;
            this.navigationItems.set([...items]);
        }
    }

    onMenuItemHover(index: number) {
        // Clear any existing timeout
        if (this.hoverTimeout) {
            clearTimeout(this.hoverTimeout);
            this.hoverTimeout = null;
        }
    
        if (this.isCollapsed() && !this.isMobileOpen()) {
            this.hoveredItemIndex.set(index);
        }
    }

    onMenuItemLeave() {
        // Add a small delay before hiding to allow mouse movement to submenu
        this.hoverTimeout = setTimeout(() => {
            this.hoveredItemIndex.set(null);
        }, 150);
    }

    onSubmenuEnter() {
        // Clear the timeout when mouse enters submenu
        if (this.hoverTimeout) {
            clearTimeout(this.hoverTimeout);
            this.hoverTimeout = null;
        }
    }

    onSubmenuLeave() {
        // Hide immediately when leaving submenu
        this.hoveredItemIndex.set(null);
    }
}
