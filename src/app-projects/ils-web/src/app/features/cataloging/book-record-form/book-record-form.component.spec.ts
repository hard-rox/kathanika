import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookRecordFormComponent } from './book-record-form.component';

describe('BookRecordFormComponent', () => {
  let component: BookRecordFormComponent;
  let fixture: ComponentFixture<BookRecordFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookRecordFormComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookRecordFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
