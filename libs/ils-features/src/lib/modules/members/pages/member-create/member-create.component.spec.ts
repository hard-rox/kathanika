import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MemberCreateComponent } from "./member-create.component";
import { MemberFormComponent } from '../../components/member-form/member-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { KnDateInput, KnPanel, KnTextInput, KnTextareaInput, KnToggle } from "@kathanika/kn-ui";
import { CreateMemberGQL, mockMutationGql } from '@kathanika/graphql-ts-client';

describe('MemberCreateComponent', () => {
  let component: MemberCreateComponent;
  let fixture: ComponentFixture<MemberCreateComponent>;

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
});
