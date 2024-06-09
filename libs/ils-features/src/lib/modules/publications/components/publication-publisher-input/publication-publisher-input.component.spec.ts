import { ComponentFixture, TestBed } from '@angular/core/testing';
import { PublicationPublisherInputComponent } from './publication-publisher-input.component';

describe('PublicationPublisherInputComponent', () => {
  let component: PublicationPublisherInputComponent;
  let fixture: ComponentFixture<PublicationPublisherInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PublicationPublisherInputComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(PublicationPublisherInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});
