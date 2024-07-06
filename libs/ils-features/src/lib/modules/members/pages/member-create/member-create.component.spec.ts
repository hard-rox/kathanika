import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MemberCreateComponent } from "./member-create.component";
import { MemberFormComponent } from '../../components/member-form/member-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FileServerModule, KnDateInput, KnFileInput, KnPanel, KnTextInput, KnTextareaInput, KnToggle } from "@kathanika/kn-ui";
import { CreateMemberGQL, CreateMemberInput, CreateMemberMutation, mockMutationGql } from '@kathanika/graphql-ts-client';
import { of } from 'rxjs';
import { MutationResult } from 'apollo-angular';

describe('MemberCreateComponent', () => {
  let component: MemberCreateComponent;
  let fixture: ComponentFixture<MemberCreateComponent>;
  const formValue: CreateMemberInput = {
    firstName: 'Hello',
    lastName: 'World',
    contactNumber: '01222',
    email: 'a@a.a',
    address: 'abc 123',
    dateOfBirth: new Date(),
    photoFileId: ''
  }

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberCreateComponent, MemberFormComponent],
      imports: [
        ReactiveFormsModule,
        KnPanel,
        KnTextInput,
        KnDateInput,
        KnTextareaInput,
        KnToggle,
        FileServerModule.forRoot(''),
        KnFileInput
      ],
      providers: [
        {
          provide: CreateMemberGQL,
          useValue: mockMutationGql,
        },
      ],
    });
    fixture = TestBed.createComponent(MemberCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should handle form submission with valid data', () => {
    const result: MutationResult<CreateMemberMutation> = {
      data: {
        createMember: {
          data: { id: '123' },
          message: 'Member added.',
          errors: null
        }
      }
    };

    jest.spyOn(component['gql'], 'mutate').mockReturnValue(of(result));

    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component['router'], 'navigate');

    component.onValidFormSubmit(formValue);

    expect(component['gql'].mutate).toHaveBeenCalledWith({ createMemberInput: formValue });
    expect(component['alertService'].showToast).toHaveBeenCalledWith('success', 'Member added.');
    expect(component['router'].navigate).toHaveBeenCalledWith(['/members/123']);
    expect(component.isPanelLoading).toBe(false);
    expect(component.errors).toEqual([]);
  });

  it('should handle form submission with errors', () => {
    const result: MutationResult<CreateMemberMutation> = {
      data: {
        createMember: {
          data: null,
          message: null,
          errors: [{ fieldName: 'field1', message: 'Error message 1' }]
        }
      }
    };

    jest.spyOn(component['gql'], 'mutate').mockReturnValue(of(result));

    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component['router'], 'navigate');

    component.onValidFormSubmit(formValue);

    expect(component['gql'].mutate).toHaveBeenCalledWith({ createMemberInput: formValue });
    expect(component['alertService'].showToast).not.toHaveBeenCalled();
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBe(false);
    expect(component.errors).toEqual(['field1 - Error message 1']);
  });
});
