import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KnAlert } from './alert.component';

describe('KnAlert', () => {
  let component: KnAlert;
  let fixture: ComponentFixture<KnAlert>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [KnAlert],
    });
    fixture = TestBed.createComponent(KnAlert);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should emit onClosed output', () => {
    const onClosedSpy = jest.spyOn(component['closed'], 'emit');
    component.close();
    expect(onClosedSpy).toHaveBeenCalledTimes(1);
  });
});
