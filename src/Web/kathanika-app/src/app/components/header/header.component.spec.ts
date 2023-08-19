import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderComponent } from './header.component';

describe('HeaderComponent', () => {
  let component: HeaderComponent;
  let fixture: ComponentFixture<HeaderComponent>;
  let nativeElement: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HeaderComponent],
    })
      .compileComponents()
      .then(() => {
        fixture = TestBed.createComponent(HeaderComponent);
        component = fixture.componentInstance;
        nativeElement = fixture.nativeElement as HTMLElement;
      });
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should change isNotificationVisible value when notification button clicked', () => {
    const oldValue = component.isNotificationVisible;
    const notificationBtn = nativeElement.querySelector('#notification-btn') as HTMLElement;
    notificationBtn.click();
    expect(component.isNotificationVisible).not.toEqual(oldValue);
  });

  it('should change isUserActionsVisible value when user name dropdown clicked', () => {
    const oldValue = component.isUserActionsVisible;
    const btn = nativeElement.querySelector('#user-action-btn') as HTMLElement;
    btn.click();
    expect(component.isUserActionsVisible).not.toEqual(oldValue);
  });

  it('should dispay notification container when notification button clicked', () => {
    const notificationBtn = nativeElement.querySelector('#notification-btn') as HTMLElement;
    notificationBtn.click();
    fixture.detectChanges();
    const notificationContainer = nativeElement.querySelector('#notification-container');
    expect(notificationContainer).toBeTruthy();
  });

  it('should hide notification container when notification button clicked twice', () => {
    const notificationBtn = nativeElement.querySelector('#notification-btn') as HTMLElement;
    notificationBtn.click();
    notificationBtn.click();
    fixture.detectChanges();
    const notificationContainer = nativeElement.querySelector('#notification-container');
    expect(notificationContainer).toBeFalsy();
  });

  it('should dispay user action container when user name dropdown clicked', () => {
    const userActionBtn = nativeElement.querySelector('#user-action-btn') as HTMLElement;
    userActionBtn.click();
    fixture.detectChanges();
    const userActionContainer = nativeElement.querySelector('#user-action-container');
    expect(userActionContainer).toBeTruthy();
  });

  it('should hide user action container when user name dropdown clicked twice', () => {
    const userActionBtn = nativeElement.querySelector('#user-action-btn') as HTMLElement;
    userActionBtn.click();
    userActionBtn.click();
    fixture.detectChanges();
    const userActionContainer = nativeElement.querySelector('#user-action-container');
    expect(userActionContainer).toBeFalsy();
  });
});
