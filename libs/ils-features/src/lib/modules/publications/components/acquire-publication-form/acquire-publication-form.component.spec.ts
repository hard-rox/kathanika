import { AcquirePublicationFormComponent } from "./acquire-publication-form.component";
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { AcquisitionMethod } from '@kathanika/graphql-ts-client';

describe('AcquirePublicationFormComponent', () => {
  let component: AcquirePublicationFormComponent;
  let fixture: ComponentFixture<AcquirePublicationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AcquirePublicationFormComponent],
      imports: [ReactiveFormsModule],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AcquirePublicationFormComponent);
    component = fixture.componentInstance;
    component.ngOnInit();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should update formGroup on acquisition method Purchase', () => {
    component['formGroup'].controls.acquisitionMethod.patchValue(AcquisitionMethod.Purchase);

    expect(component['formGroup'].controls.unitPrice).not.toBeNull();
    expect(component['formGroup'].controls.vendor).not.toBeNull();
    expect(component['formGroup'].controls.patron).toBeUndefined();
  });

  it('should update formGroup on acquisition method Donation', () => {
    component['formGroup'].controls.acquisitionMethod.patchValue(AcquisitionMethod.Donation);

    expect(component['formGroup'].controls.unitPrice).toBeUndefined();
    expect(component['formGroup'].controls.vendor).toBeUndefined();
    expect(component['formGroup'].controls.patron).not.toBeNull();
  });
});
