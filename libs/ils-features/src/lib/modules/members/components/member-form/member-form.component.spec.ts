import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberFormComponent } from './member-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { KnDateInput, KnTextInput, KnTextareaInput } from '@kathanika/kn-ui';

describe('MemberFormComponent', () => {
  let component: MemberFormComponent;
  let fixture: ComponentFixture<MemberFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberFormComponent],
      imports: [
        ReactiveFormsModule,
        KnTextInput,
        KnDateInput,
        KnTextareaInput
      ]
    });
    fixture = TestBed.createComponent(MemberFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
