import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberUpdateComponent } from './member-update.component';
import { GetMemberGQL, UpdateMemberGQL, mockMutationGql, mockQueryGql } from '@kathanika/graphql-ts-client';
import { ReactiveFormsModule } from '@angular/forms';
import { KnDateInput, KnPanel, KnTextInput, KnTextareaInput, KnToggle } from '@kathanika/kn-ui';
import { ActivatedRoute } from '@angular/router';
import { MemberFormComponent } from '../../components/member-form/member-form.component';

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
});
