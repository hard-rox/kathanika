import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KnNumberInput } from './number-input.component';

describe('KnNumberInput', () => {
  let component: KnNumberInput;
  let fixture: ComponentFixture<KnNumberInput>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [KnNumberInput],
    });
    fixture = TestBed.createComponent(KnNumberInput);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
