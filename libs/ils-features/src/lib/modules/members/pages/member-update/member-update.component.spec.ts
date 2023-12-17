import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberUpdateComponent } from './member-update.component';
import { UpdateMemberGQL } from '@kathanika/graphql-ts-client';
import { mockMutationGql } from "../../../../test-utils/gql-test-utils";
import { ReactiveFormsModule } from '@angular/forms';
import { KnDateInput, KnPanel, KnTextInput, KnTextareaInput, KnToggle } from '@kathanika/kn-ui';

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
      ],
      declarations: [MemberUpdateComponent],
      providers: [
        {
          provide: UpdateMemberGQL,
          useValue: mockMutationGql,
        },
      ],
    });
    fixture = TestBed.createComponent(MemberUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});
