import { Component, ViewChild } from '@angular/core';

@Component({
  selector: 'kn-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  isUserActionsVisible: boolean = false;
  isNotificationVisible: boolean = false;
}
