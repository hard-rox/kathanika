import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherFormComponent } from './publisher-form.component';
import {
  KnTextInput,
  KnTextareaInput,
  KnChip,
} from '@kathanika/kn-ui';

describe('PublisherFormComponent', () => {
  let component: PublisherFormComponent;
  let fixture: ComponentFixture<PublisherFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublisherFormComponent],
      imports: [
        KnTextInput,
        KnTextareaInput,
        KnChip,
      ],
    });
    fixture = TestBed.createComponent(PublisherFormComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
