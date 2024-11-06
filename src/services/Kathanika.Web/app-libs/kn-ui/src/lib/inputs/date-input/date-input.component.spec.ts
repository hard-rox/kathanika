import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KnDateInput } from './date-input.component';

describe('KnDateInput', () => {
  let component: KnDateInput;
  let fixture: ComponentFixture<KnDateInput>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [KnDateInput],
    });
    fixture = TestBed.createComponent(KnDateInput);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
