import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherFormComponent } from './publisher-form.component';
import { TextInputComponent } from 'src/app/shared/components/text-input/text-input.component';
import { TextareaInputComponent } from 'src/app/shared/components/textarea-input/textarea-input.component';

describe('PublisherFormComponent', () => {
  let component: PublisherFormComponent;
  let fixture: ComponentFixture<PublisherFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublisherFormComponent],
      imports: [
        TextInputComponent,
        TextareaInputComponent
      ]
    });
    fixture = TestBed.createComponent(PublisherFormComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
