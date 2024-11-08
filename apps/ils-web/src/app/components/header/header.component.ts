import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { KnButton } from '@kathanika/kn-ui';

@Component({
    selector: 'kn-ils-web-header',
    standalone: true,
    imports: [CommonModule, KnButton],
    templateUrl: './header.component.html',
})
export class HeaderComponent {
    isUserActionsVisible = false;
    isNotificationVisible = false;
}
