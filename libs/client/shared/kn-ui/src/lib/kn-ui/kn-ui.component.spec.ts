import { ComponentFixture, TestBed } from '@angular/core/testing';
import { KnUiComponent } from './kn-ui.component';

describe('KnUiComponent', () => {
  let component: KnUiComponent;
  let fixture: ComponentFixture<KnUiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [KnUiComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(KnUiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
