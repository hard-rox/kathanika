import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberUpdateComponent } from './member-update.component';
import { GetMemberGQL, GetMemberQuery, MemberPatchInput, MembershipStatus, UpdateMemberGQL, UpdateMemberMutation, mockMutationGql, mockQueryGql } from '@kathanika/graphql-ts-client';
import { ReactiveFormsModule } from '@angular/forms';
import { FileServerModule, KnDateInput, KnFileInput, KnPanel, KnTextInput, KnTextareaInput, KnToggle } from '@kathanika/kn-ui';
import { ActivatedRoute } from '@angular/router';
import { MemberFormComponent } from '../../components/member-form/member-form.component';
import { of, throwError } from 'rxjs';
import { ApolloQueryResult } from '@apollo/client';
import { MutationResult } from 'apollo-angular';

describe('MemberUpdateComponent', () => {
  let component: MemberUpdateComponent;
  let fixture: ComponentFixture<MemberUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
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
      declarations: [
        MemberUpdateComponent,
        MemberFormComponent
      ],
      providers: [
        {
          provide: UpdateMemberGQL,
          useValue: mockMutationGql,
        },
        {
          provide: GetMemberGQL,
          useValue: mockQueryGql,
        },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: {
                id: '123',
              },
            },
          },
        }
      ],
    });
    fixture = TestBed.createComponent(MemberUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load member data on component initialization', () => {
    const mockQueryResult: ApolloQueryResult<GetMemberQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        member: {
          id: '123',
          firstName: 'John',
          lastName: 'Doe',
          contactNumber: '12355',
          address: 'Abc123',
          dateOfBirth: new Date(),
          email: 'a@a.a',
          fullName: 'John Doe',
          membershipStartDateTime: new Date(),
          status: MembershipStatus.Active
        },
      },
    };

    jest.spyOn(component['gql'], 'fetch').mockReturnValueOnce(of(mockQueryResult));

    jest.spyOn(component['alertService'], 'showPopup');
    jest.spyOn(component['router'], 'navigate');

    component.ngOnInit();

    expect(component['gql'].fetch).toHaveBeenCalledWith({ id: '123' });
    expect(component['alertService'].showPopup).not.toHaveBeenCalled();
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBeFalsy();
    expect(component.memberToUpdate).toEqual(mockQueryResult.data.member);
  });

  it('should handle error when loading member data', () => {
    const mockQueryResult: ApolloQueryResult<GetMemberQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        member: null
      },
      error: {
        cause: null,
        graphQLErrors: [],
        protocolErrors: [],
        clientErrors: [],
        networkError: null,
        message: 'An error occurred',
        name: 'Test',
        extraInfo: { additionalData: 'Additional information' },
      }
    };

    jest.spyOn(component['gql'], 'fetch').mockReturnValueOnce(of(mockQueryResult));

    jest.spyOn(component['alertService'], 'showPopup');
    jest.spyOn(component['router'], 'navigate');

    component.ngOnInit();

    expect(component['gql'].fetch).toHaveBeenCalledWith({ id: '123' });
    expect(component['alertService'].showPopup).toHaveBeenCalledWith('error', mockQueryResult.error?.message);
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.memberToUpdate).toBeNull();
  });

  it('should handle member not found', () => {
    const mockQueryResult: ApolloQueryResult<GetMemberQuery> = {
      loading: false,
      networkStatus: 7,
      data: {
        member: null
      }
    };
    jest.spyOn(component['gql'], 'fetch').mockReturnValueOnce(of(mockQueryResult));

    jest.spyOn(component['alertService'], 'showPopup');
    jest.spyOn(component['router'], 'navigate');

    component.ngOnInit();

    expect(component['gql'].fetch).toHaveBeenCalledWith({ id: '123' });
    expect(component['alertService'].showPopup).toHaveBeenCalledWith('error', 'Returning to list page.', 'Member not found');
    expect(component['router'].navigate).toHaveBeenCalledWith(['/members']);
    expect(component.memberToUpdate).toBeNull();
  });

  it('should handle form submission with successful result', () => {
    const result = {
      data: {
        updateMember: {
          data: { id: '123' },
          message: 'Member updated.',
          errors: null
        }
      }
    };

    jest.spyOn(component['mutation'], 'mutate').mockReturnValueOnce(of(result));
    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component.memberUpdateForm, 'resetForm');
    jest.spyOn(component['router'], 'navigate');

    component.memberId = '123';
    const memberOutput = {} as MemberPatchInput;

    component.onValidFormSubmit(memberOutput);

    expect(component['mutation'].mutate).toHaveBeenCalledWith({
      id: '123',
      memberPatch: memberOutput
    });
    expect(component['alertService'].showToast).toHaveBeenCalledWith('success', 'Member updated.');
    expect(component.memberUpdateForm.resetForm).toHaveBeenCalled();
    expect(component['router'].navigate).toHaveBeenCalledWith(['/members/123']);
  });

  it('should handle form submission with errors', () => {
    const result: MutationResult<UpdateMemberMutation> = {
      data: {
        updateMember: {
          data: null,
          message: null,
          errors: [
            { __typename: 'InvalidFieldError', fieldName: 'fieldName1', message: 'Invalid field 1' }
          ]
        }
      }
    };

    jest.spyOn(component['mutation'], 'mutate').mockReturnValueOnce(of(result));
    jest.spyOn(component['alertService'], 'showToast');
    jest.spyOn(component.memberUpdateForm, 'resetForm');
    jest.spyOn(component['router'], 'navigate');

    component.memberId = '123';
    const memberOutput = {} as MemberPatchInput;

    component.onValidFormSubmit(memberOutput);

    expect(component['mutation'].mutate).toHaveBeenCalledWith({
      id: '123',
      memberPatch: memberOutput
    });
    expect(component['alertService'].showToast).not.toHaveBeenCalled();
    expect(component.memberUpdateForm.resetForm).not.toHaveBeenCalled();
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBe(false);
    expect(component.errors.length).toEqual(1);
  });

  it('should handle form submission with network error', () => {
    const error = new Error('Network error');
    jest.spyOn(component['mutation'], 'mutate')
      .mockReturnValueOnce(throwError(() => error));
    jest.spyOn(component['alertService'], 'showHttpErrorPopup');
    jest.spyOn(component['router'], 'navigate');

    component.memberId = '123';
    const memberOutput = {} as MemberPatchInput;

    component.onValidFormSubmit(memberOutput);

    expect(component['mutation'].mutate).toHaveBeenCalledWith({
      id: '123',
      memberPatch: memberOutput
    });
    expect(component['alertService'].showHttpErrorPopup).toHaveBeenCalledWith(error);
    expect(component['router'].navigate).not.toHaveBeenCalled();
    expect(component.isPanelLoading).toBe(false);
  });
});
