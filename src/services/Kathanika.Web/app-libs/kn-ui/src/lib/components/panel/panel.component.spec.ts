import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KnPanel } from './panel.component';

describe('KnPanel', () => {
  let component: KnPanel;
  let fixture: ComponentFixture<KnPanel>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [KnPanel],
    });
    fixture = TestBed.createComponent(KnPanel);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
