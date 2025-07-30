import { TestBed } from '@angular/core/testing';
import { SidebarService } from './sidebar.service';

describe('SidebarService', () => {
  let service: SidebarService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SidebarService);

    // Mock window.innerWidth for testing both mobile and desktop scenarios
    Object.defineProperty(window, 'innerWidth', {
      writable: true,
      configurable: true,
      value: 1200 // Default to desktop size
    });
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should initialize with isCollapsed as false', () => {
    expect(service.isCollapsed()).toBe(false);
  });

  it('should initialize with isMobileOpen as false', () => {
    expect(service.isMobileOpen()).toBe(false);
  });

  describe('Desktop mode', () => {
    beforeEach(() => {
      window.innerWidth = 1200; // Desktop size (≥ 1024px)
    });

    it('should toggle isCollapsed on desktop', () => {
      // Initially false
      expect(service.isCollapsed()).toBe(false);

      // Toggle to true
      service.toggle();
      expect(service.isCollapsed()).toBe(true);

      // Toggle back to false
      service.toggle();
      expect(service.isCollapsed()).toBe(false);
    });

    it('should collapse sidebar on desktop', () => {
      service.collapse();
      expect(service.isCollapsed()).toBe(true);
    });

    it('should expand sidebar on desktop', () => {
      // First collapse it
      service.collapse();
      expect(service.isCollapsed()).toBe(true);

      // Then expand it
      service.expand();
      expect(service.isCollapsed()).toBe(false);
    });
  });

  describe('Mobile mode', () => {
    beforeEach(() => {
      window.innerWidth = 768; // Mobile size (< 1024px)
    });

    it('should toggle isMobileOpen on mobile', () => {
      // Initially false
      expect(service.isMobileOpen()).toBe(false);

      // Toggle to true
      service.toggle();
      expect(service.isMobileOpen()).toBe(true);

      // Toggle back to false
      service.toggle();
      expect(service.isMobileOpen()).toBe(false);
    });

    it('should close mobile sidebar', () => {
      // First open it
      service.openMobile();
      expect(service.isMobileOpen()).toBe(true);

      // Then close it
      service.closeMobile();
      expect(service.isMobileOpen()).toBe(false);
    });

    it('should open mobile sidebar', () => {
      service.openMobile();
      expect(service.isMobileOpen()).toBe(true);
    });
  });
});
