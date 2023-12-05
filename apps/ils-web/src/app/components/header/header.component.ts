import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  selector: 'kathanika-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent {
  isUserActionsVisible: boolean = false;
  isNotificationVisible: boolean = false;
}
