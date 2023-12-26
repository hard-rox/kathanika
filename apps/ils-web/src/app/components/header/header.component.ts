import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { KnButton } from '@kathanika/kn-ui';

@Component({
  selector: 'kathanika-header',
  standalone: true,
  imports: [CommonModule, KnButton],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  isUserActionsVisible: boolean = false;
  isNotificationVisible: boolean = false;
}
