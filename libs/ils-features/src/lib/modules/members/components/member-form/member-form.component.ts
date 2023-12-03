import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'kn-member-form',
  templateUrl: './member-form.component.html',
  styleUrls: ['./member-form.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MemberFormComponent {}
