import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseItemFormComponent } from './purchase-item-form.component';

describe('PurchaseItemFormComponent', () => {
  let component: PurchaseItemFormComponent;
  let fixture: ComponentFixture<PurchaseItemFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PurchaseItemFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchaseItemFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
