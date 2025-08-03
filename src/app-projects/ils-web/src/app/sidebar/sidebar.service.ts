import { Injectable, PLATFORM_ID, Inject, signal } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  private readonly _isCollapsed = signal(false);
  private readonly _isMobileOpen = signal(false);

  // Read-only signals for components to subscribe to
  readonly isCollapsed = this._isCollapsed.asReadonly();
  readonly isMobileOpen = this._isMobileOpen.asReadonly();

  constructor(@Inject(PLATFORM_ID) private platformId: Object) {}

  toggle(): void {
    if (isPlatformBrowser(this.platformId)) {
      const isMobile = window.innerWidth < 768;
      this._isMobileOpen.set(!this._isMobileOpen());

      if (!isMobile) {
        localStorage.setItem('sidebarOpen', String(this._isMobileOpen()));
      }
    }
  }

  collapse() {
    this._isCollapsed.set(true);
  }

  expand() {
    this._isCollapsed.set(false);
  }

  closeMobile() {
    this._isMobileOpen.set(false);
  }

  openMobile() {
    this._isMobileOpen.set(true);
  }
}
