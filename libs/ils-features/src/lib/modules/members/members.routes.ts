import { Routes } from '@angular/router';
import { MemberCreateComponent } from './pages/member-create/member-create.component';
import { MemberDetailsComponent } from './pages/member-details/member-details.component';
import { MemberListComponent } from './pages/member-list/member-list.component';
import { MemberUpdateComponent } from './pages/member-update/member-update.component';

export const routes: Routes = [
  {
    path: '',
    component: MemberListComponent,
  },
  {
    path: 'add',
    component: MemberCreateComponent,
  },
  {
    path: 'update/:id',
    component: MemberUpdateComponent,
  },
  {
    path: ':id',
    component: MemberDetailsComponent,
  },
];
