import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { AuthorFormComponent } from './author-form.component';

describe('AuthorFormComponent', () => {
  let component: AuthorFormComponent;
  let fixture: ComponentFixture<AuthorFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      declarations: [AuthorFormComponent],
    });
    fixture = TestBed.createComponent(AuthorFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set update to "true" on setting author input', () => {
    component.author = {
      firstName: 'Hello',
      lastName: 'World',
      dateOfBirth: '2023-01-01',
      dateOfDeath: null,
      biography: '',
      nationality: ''
    }

    expect(component.isUpdate).toBeTrue();
  });

  it('should patch authorFormGroup on setting author input', () => {
    const author = {
      firstName: 'Hello',
      lastName: 'World',
      dateOfBirth: '2023-01-01',
      dateOfDeath: null,
      biography: '',
      nationality: ''
    }

    component.author = author;

    expect(component.authorFromGroup.value).toEqual(author);
  });

  it('should mark all field as touched on invalid submit', () => {
    component.submitForm();
    expect(component.authorFromGroup.touched).toBeTrue();
  });

  it('should emit "onSubmit" on valid submit', () => {
    const onSubmitSpy = spyOn(component.onSubmit, 'emit');
    const author = {
      firstName: 'Hello',
      lastName: 'World',
      dateOfBirth: '2023-01-01',
      dateOfDeath: null,
      biography: 'test',
      nationality: 'test'
    }
    component.authorFromGroup.setValue(author);
    component.submitForm();
    expect(onSubmitSpy).toHaveBeenCalledTimes(1);
  });

  it('should reset formGroup on reset() call', () => {
    const formGroupResetSpy = spyOn(component.authorFromGroup, 'reset');
    component.resetForm();
    expect(formGroupResetSpy).toHaveBeenCalledTimes(1);
  });
});
