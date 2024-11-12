import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KnChip } from './chip.component';

describe('KnChip', () => {
  let component: KnChip;
  let fixture: ComponentFixture<KnChip>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [KnChip],
    });
    fixture = TestBed.createComponent(KnChip);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
