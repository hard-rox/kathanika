import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RecordPurchaseComponent } from './record-purchase.component';

describe('RecordPurchaseComponent', () => {
  let component: RecordPurchaseComponent;
  let fixture: ComponentFixture<RecordPurchaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecordPurchaseComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(RecordPurchaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});
