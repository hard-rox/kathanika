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
            const notificationBtn = nativeElement.querySelector('button[aria-label="View notifications"]') as HTMLElement;
            
            // Initially notification container should not be visible
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-80')).toBe(null);
            
            // Click to show notifications
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-80')).toBeTruthy();
        });

        it('should close user actions when notifications are opened', () => {
            const notificationBtn = nativeElement.querySelector('button[aria-label="View notifications"]') as HTMLElement;
            const userActionBtn = nativeElement.querySelector('button[aria-label="User actions"]') as HTMLElement;
            
            // First open user actions
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-56')).toBeTruthy();
            
            // Then open notifications - should close user actions
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-56')).toBe(null);
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-80')).toBeTruthy();
        });

        it('should toggle notification container visibility', () => {
            const notificationBtn = nativeElement.querySelector('button[aria-label="View notifications"]') as HTMLElement;
            
            // Open notifications
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-80')).toBeTruthy();
            
            // Close notifications
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-80')).toBe(null);
        });

        it('should display notification badge with count', () => {
            const notificationBadge = nativeElement.querySelector('.absolute.-top-1.-right-1.bg-red-500') as HTMLElement;
            
            expect(notificationBadge).toBeTruthy();
            expect(notificationBadge.textContent?.trim()).toBe('3');
        });
    });

    describe('User Actions', () => {
        it('should toggle user actions visibility when user button clicked', () => {
            const userActionBtn = nativeElement.querySelector('button[aria-label="User actions"]') as HTMLElement;
            
            // Initially user action container should not be visible
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-56')).toBe(null);
            
            // Click to show user actions
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-56')).toBeTruthy();
        });

        it('should close notifications when user actions are opened', () => {
            const notificationBtn = nativeElement.querySelector('button[aria-label="View notifications"]') as HTMLElement;
            const userActionBtn = nativeElement.querySelector('button[aria-label="User actions"]') as HTMLElement;
            
            // First open notifications
            notificationBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-80')).toBeTruthy();
            
            // Then open user actions - should close notifications
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-80')).toBe(null);
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-56')).toBeTruthy();
        });

        it('should toggle user actions container visibility', () => {
            const userActionBtn = nativeElement.querySelector('button[aria-label="User actions"]') as HTMLElement;
            
            // Open user actions
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-56')).toBeTruthy();
            
            // Close user actions
            userActionBtn.click();
            fixture.detectChanges();
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-56')).toBe(null);
        });

        it('should display correct expand/collapse icon', () => {
            const userActionBtn = nativeElement.querySelector('button[aria-label="User actions"]') as HTMLElement;
            const expandIcon = userActionBtn.querySelector('.material-symbols-rounded') as HTMLElement;
            
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
            const menuBtn = nativeElement.querySelector('button[aria-label="Toggle sidebar"]') as HTMLElement;
            
            menuBtn.click();
            expect(sidebarService.toggle).toHaveBeenCalled();
        });
    });

    describe('closeDropdowns', () => {
        it('should close both notifications and user actions', () => {
            const notificationBtn = nativeElement.querySelector('button[aria-label="View notifications"]') as HTMLElement;
            const userActionBtn = nativeElement.querySelector('button[aria-label="User actions"]') as HTMLElement;
            
            // Open both dropdowns
            notificationBtn.click();
            userActionBtn.click();
            fixture.detectChanges();
            
            // Call closeDropdowns method
            component.closeDropdowns();
            fixture.detectChanges();
            
            // Both should be closed
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-80')).toBe(null);
            expect(nativeElement.querySelector('.absolute.right-0.mt-2.w-56')).toBe(null);
        });
    });

    describe('Header Elements', () => {
        it('should display the application title', () => {
            const title = nativeElement.querySelector('h1') as HTMLElement;
            
            expect(title).toBeTruthy();
            expect(title.textContent?.trim()).toBe('কথনিকা');
        });

        it('should display user profile image', () => {
            const profileImg = nativeElement.querySelector('img[alt="User profile"]') as HTMLImageElement;
            
            expect(profileImg).toBeTruthy();
            expect(profileImg.src).toBe('https://picsum.photos/200');
        });

        it('should display user name', () => {
            const userName = nativeElement.querySelector('.hidden.sm\\:block') as HTMLElement;
            
            expect(userName).toBeTruthy();
            expect(userName.textContent?.trim()).toBe('John Doe');
        });
    });
});