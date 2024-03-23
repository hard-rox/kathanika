import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PurchaseRecordFormComponent } from './purchase-record-form.component';

describe('PurchaseRecordFormComponent', () => {
  let component: PurchaseRecordFormComponent;
  let fixture: ComponentFixture<PurchaseRecordFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PurchaseRecordFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PurchaseRecordFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
