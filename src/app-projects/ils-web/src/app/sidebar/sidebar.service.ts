import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SidebarService {
  private readonly _isCollapsed = signal(false);
  private readonly _isMobileOpen = signal(false);

  // Read-only signals for components to subscribe to
  readonly isCollapsed = this._isCollapsed.asReadonly();
  readonly isMobileOpen = this._isMobileOpen.asReadonly();

  toggle() {
    // On mobile (< lg), toggle mobile sidebar
    // On desktop (>= lg), toggle collapsed state
    if (window.innerWidth < 1024) {
      this._isMobileOpen.set(!this._isMobileOpen());
    } else {
      this._isCollapsed.set(!this._isCollapsed());
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
