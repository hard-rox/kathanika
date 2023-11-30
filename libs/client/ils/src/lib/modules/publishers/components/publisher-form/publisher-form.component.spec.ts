import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublisherFormComponent } from './publisher-form.component';
import { TextInputComponent, TextareaInputComponent, SearchbarModule, ChipComponent } from '@kathanika/kn-ui';

describe('PublisherFormComponent', () => {
  let component: PublisherFormComponent;
  let fixture: ComponentFixture<PublisherFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublisherFormComponent],
      imports: [
        TextInputComponent,
        TextareaInputComponent,
        SearchbarModule,
        ChipComponent
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
