import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PurchaseOrderDetailsComponent } from './purchase-order-details.component';

describe('PurchaseOrderDetailsComponent', () => {
  let component: PurchaseOrderDetailsComponent;
  let fixture: ComponentFixture<PurchaseOrderDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PurchaseOrderDetailsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PurchaseOrderDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
