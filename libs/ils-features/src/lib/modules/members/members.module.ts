import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MemberListComponent } from './pages/member-list/member-list.component';
import { MemberAddComponent } from './pages/member-add/member-add.component';
import { MemberDetailsComponent } from './pages/member-details/member-details.component';
import { MemberUpdateComponent } from './pages/member-update/member-update.component';
import { MemberFormComponent } from './components/member-form/member-form.component';
import { RouterModule } from '@angular/router';
import { routes } from './members.routes';
import {
  BadgeComponent,
  ButtonDirective,
  PaginationComponent,
  PanelComponent,
} from '@kathanika/kn-ui';

@NgModule({
  declarations: [
    MemberListComponent,
    MemberAddComponent,
    MemberDetailsComponent,
    MemberUpdateComponent,
    MemberFormComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    PaginationComponent,
    BadgeComponent,
    PanelComponent,
    ButtonDirective
  ],
})
export class MembersModule {}
