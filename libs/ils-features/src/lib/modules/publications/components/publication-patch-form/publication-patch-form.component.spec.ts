import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PublicationPatchFormComponent } from './publication-patch-form.component';

describe('PublicationPatchFormComponent', () => {
  let component: PublicationPatchFormComponent;
  let fixture: ComponentFixture<PublicationPatchFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PublicationPatchFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PublicationPatchFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
