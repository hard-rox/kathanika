import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HeaderComponent } from './header.component';
import { SidebarService } from '../sidebar/sidebar.service';

describe('HeaderComponent', () => {
    let component: HeaderComponent;
    let fixture: ComponentFixture<HeaderComponent>;
    let nativeElement: HTMLElement;
    let sidebarService: jest.Mocked<SidebarService>;

    beforeEach(async () => {
        const mockSidebarService = {
            toggle: jest.fn()
        };
        
        await TestBed.configureTestingModule({
            imports: [HeaderComponent],
            providers: [
                { provide: SidebarService, useValue: mockSidebarService }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(HeaderComponent);
        component = fixture.componentInstance;
        nativeElement = fixture.nativeElement as HTMLElement;
        sidebarService = TestBed.inject(SidebarService) as jest.Mocked<SidebarService>;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    describe('Notifications', () => {
        it('should toggle notification visibility when notification button clicked', () => {
            const notificationBtn = nativeElement.querySelector('#notification-btn') as HTMLElement;

            // Initially notification container should not be visible
            expect(nativeElement.querySelector('#notification-dropdown')).toBe(null);

            // Click to show notifications
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#notification-dropdown')).toBeTruthy();
        });

        it('should close user actions when notifications are opened', () => {
            const notificationBtn = nativeElement.querySelector('#notification-btn') as HTMLElement;
            const userActionBtn = nativeElement.querySelector('#user-actions-btn') as HTMLElement;

            // First open user actions
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#user-actions-dropdown')).toBeTruthy();

            // Then open notifications - should close user actions
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#user-actions-dropdown')).toBe(null);
            expect(nativeElement.querySelector('#notification-dropdown')).toBeTruthy();
        });

        it('should toggle notification container visibility', () => {
            const notificationBtn = nativeElement.querySelector('#notification-btn') as HTMLElement;

            // Open notifications
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#notification-dropdown')).toBeTruthy();

            // Close notifications
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#notification-dropdown')).toBe(null);
        });

        it('should display notification badge with count', () => {
            const notificationBadge = nativeElement.querySelector('#notification-badge') as HTMLElement;
            
            expect(notificationBadge).toBeTruthy();
            expect(notificationBadge.textContent?.trim()).toBe('3');
        });
    });

    describe('User Actions', () => {
        it('should toggle user actions visibility when user button clicked', () => {
            const userActionBtn = nativeElement.querySelector('#user-actions-btn') as HTMLElement;

            // Initially user action container should not be visible
            expect(nativeElement.querySelector('#user-actions-dropdown')).toBe(null);

            // Click to show user actions
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#user-actions-dropdown')).toBeTruthy();
        });

        it('should close notifications when user actions are opened', () => {
            const notificationBtn = nativeElement.querySelector('#notification-btn') as HTMLElement;
            const userActionBtn = nativeElement.querySelector('#user-actions-btn') as HTMLElement;

            // First open notifications
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#notification-dropdown')).toBeTruthy();

            // Then open user actions - should close notifications
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#notification-dropdown')).toBe(null);
            expect(nativeElement.querySelector('#user-actions-dropdown')).toBeTruthy();
        });

        it('should toggle user actions container visibility', () => {
            const userActionBtn = nativeElement.querySelector('#user-actions-btn') as HTMLElement;

            // Open user actions
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#user-actions-dropdown')).toBeTruthy();

            // Close user actions
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('#user-actions-dropdown')).toBe(null);
        });

        it('should display correct expand/collapse icon', () => {
            const userActionBtn = nativeElement.querySelector('#user-actions-btn') as HTMLElement;
            const expandIcon = nativeElement.querySelector('#expand-icon') as HTMLElement;
            
            // Initially should show expand_more
            expect(expandIcon.textContent?.trim()).toBe('expand_more');
            
            // After clicking should show expand_less
            userActionBtn.click();
            fixture.detectChanges();
            expect(expandIcon.textContent?.trim()).toBe('expand_less');
        });
    });

    describe('Sidebar', () => {
        it('should call sidebar service toggle when toggleSidebar is called', () => {
            component.toggleSidebar();
            expect(sidebarService.toggle).toHaveBeenCalled();
        });

        it('should call sidebar service toggle when menu button is clicked', () => {
            const menuBtn = nativeElement.querySelector('#sidebar-toggle-btn') as HTMLElement;

            menuBtn.click();
            expect(sidebarService.toggle).toHaveBeenCalled();
        });
    });

    describe('closeDropdowns', () => {
        it('should close both notifications and user actions', () => {
            const notificationBtn = nativeElement.querySelector('#notification-btn') as HTMLElement;
            const userActionBtn = nativeElement.querySelector('#user-actions-btn') as HTMLElement;

            // Open both dropdowns
            notificationBtn.click();
            userActionBtn.click();
            fixture.detectChanges();

            // Call closeDropdowns method
            component.closeDropdowns();
            fixture.detectChanges();

            // Both should be closed
            expect(nativeElement.querySelector('#notification-dropdown')).toBe(null);
            expect(nativeElement.querySelector('#user-actions-dropdown')).toBe(null);
        });
    });

    describe('Header Elements', () => {
        it('should display the application title', () => {
            const title = nativeElement.querySelector('#app-title') as HTMLElement;

            expect(title).toBeTruthy();
            expect(title.textContent?.trim()).toBe('কথনিকা');
        });

        it('should display user profile image', () => {
            const profileImg = nativeElement.querySelector('#user-avatar') as HTMLImageElement;

            expect(profileImg).toBeTruthy();
            expect(profileImg.src).toBe('https://picsum.photos/200');
        });

        it('should display user name', () => {
            const userName = nativeElement.querySelector('#user-name') as HTMLElement;
            
            expect(userName).toBeTruthy();
            expect(userName.textContent?.trim()).toBe('John Doe');
        });
    });
});