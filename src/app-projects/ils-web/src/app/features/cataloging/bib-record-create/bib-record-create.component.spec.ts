import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BibRecordCreateComponent } from './bib-record-create.component';

xdescribe('BibRecordCreateComponent', () => {
  let component: BibRecordCreateComponent;
  let fixture: ComponentFixture<BibRecordCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BibRecordCreateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BibRecordCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
