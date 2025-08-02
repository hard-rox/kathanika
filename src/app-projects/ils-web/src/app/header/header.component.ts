import { Component, signal, inject } from '@angular/core';
import { SidebarService } from '../sidebar/sidebar.service';

@Component({
  selector: 'app-header',
  imports: [],
  templateUrl: './header.component.html'
})
export class HeaderComponent {
  private sidebarService = inject(SidebarService);
  
  protected readonly isNotificationVisible = signal(false);
  protected readonly isUserActionsVisible = signal(false);

  toggleSidebar() {
    this.sidebarService.toggle();
  }

  toggleNotifications() {
    this.isNotificationVisible.set(!this.isNotificationVisible());
    if (this.isNotificationVisible()) {
      this.isUserActionsVisible.set(false);
    }
  }

  toggleUserActions() {
    this.isUserActionsVisible.set(!this.isUserActionsVisible());
    if (this.isUserActionsVisible()) {
      this.isNotificationVisible.set(false);
    }
  }

  closeDropdowns() {
    this.isNotificationVisible.set(false);
    this.isUserActionsVisible.set(false);
  }
}
