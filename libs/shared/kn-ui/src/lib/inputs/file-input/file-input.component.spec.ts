import { ComponentFixture, TestBed } from '@angular/core/testing';
import { KnFileInput } from './file-input.component';

describe('FileInputComponent', () => {
  let component: KnFileInput;
  let fixture: ComponentFixture<KnFileInput>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KnFileInput],
    }).compileComponents();

    fixture = TestBed.createComponent(KnFileInput);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});
