import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { AuthorFormComponent } from './author-form.component';
import { TextInputComponent } from 'src/app/shared/components/text-input/text-input.component';
import { DateInputComponent } from 'src/app/shared/components/date-input/date-input.component';
import { TextareaInputComponent } from 'src/app/shared/components/textarea-input/textarea-input.component';
import { ToggleComponent } from 'src/app/shared/components/toggle/toggle.component';
import { AuthorFormOutput } from '../../types/author-form-output';

describe('AuthorFormComponent', () => {
  let component: AuthorFormComponent;
  let fixture: ComponentFixture<AuthorFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        TextInputComponent,
        DateInputComponent,
        TextareaInputComponent,
        ToggleComponent
      ],
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

  ///TODO: Move to base class
  it('should patch authorFormGroup on setting author input', () => {
    // const author: AuthorFormOutput = {
    //   firstName: 'Hello',
    //   lastName: 'World',
    //   dateOfBirth: new Date('2023-01-01'),
    //   markedAsDeceased: false,
    //   dateOfDeath: null,
    //   biography: '',
    //   nationality: ''
    // }

    // component.author = author;

    // expect(component.authorFromGroup.value).toEqual(author);
  });

  ///TODO: Move to base class
  it('should mark all field as touched on invalid submit', () => {
    // component.submitForm();
    // expect(component.authorFromGroup.touched).toBeTrue();
  });

  ///TODO: Move to base class
  it('should emit "onSubmit" on valid submit', () => {
    // const onSubmitSpy = spyOn(component.onSubmit, 'emit');
    // component.authorFromGroup.patchValue({
    //   firstName: 'Hello',
    //   lastName: 'World',
    //   dateOfBirth: new Date('2023-01-01'),
    //   markedAsDeceased: false,
    //   dateOfDeath: null,
    //   biography: ' ',
    //   nationality: ' '
    // });
    // component.submitForm();
    // expect(onSubmitSpy).toHaveBeenCalledTimes(1);
  });

  ///TODO: Move to base class
  xit('should reset formGroup on reset() call', () => {
    // const formGroupResetSpy = spyOn(component.authorFromGroup, 'reset');
    // component.resetForm();
    // expect(formGroupResetSpy).toHaveBeenCalledTimes(1);
  });
});
