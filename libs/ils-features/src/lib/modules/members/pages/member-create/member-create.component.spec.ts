import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MemberCreateComponent } from "./member-create.component";
import { MemberFormComponent } from '../../components/member-form/member-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DateInputComponent, PanelComponent, TextInputComponent, TextareaInputComponent, ToggleComponent } from "@kathanika/kn-ui";
import { CreateMemberGQL } from '@kathanika/graphql-ts-client';
import { mockMutationGql } from 'libs/ils-features/src/lib/test-utils/gql-test-utils';

describe('MemberCreateComponent', () => {
  let component: MemberCreateComponent;
  let fixture: ComponentFixture<MemberCreateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberCreateComponent, MemberFormComponent],
      imports: [
        ReactiveFormsModule,
        PanelComponent,
        TextInputComponent,
        DateInputComponent,
        TextareaInputComponent,
        ToggleComponent,
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
