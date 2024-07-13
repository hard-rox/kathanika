import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberListComponent } from './pages/member-list/member-list.component';
import { MemberCreateComponent } from './pages/member-create/member-create.component';
import { MemberDetailsComponent } from './pages/member-details/member-details.component';
import { MemberUpdateComponent } from './pages/member-update/member-update.component';
import { MemberFormComponent } from './components/member-form/member-form.component';
import { RouterModule } from '@angular/router';
import { routes } from './members.routes';
import {
  KnAlert,
  KnBadge,
  KnButton,
  KnDateInput,
  KnFileInput,
  KnPagination,
  KnPanel,
  KnTextInput,
  KnTextareaInput,
} from '@kathanika/kn-ui';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    MemberListComponent,
    MemberCreateComponent,
    MemberDetailsComponent,
    MemberUpdateComponent,
    MemberFormComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    KnPagination,
    KnBadge,
    KnPanel,
    KnButton,
    KnAlert,
    KnTextInput,
    KnDateInput,
    KnTextareaInput,
    KnFileInput
  ],
})
export class MembersModule { }
