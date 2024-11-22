import {Component} from '@angular/core';
import {KnButton} from "@kathanika/kn-ui";

@Component({
    selector: 'app-header',
    standalone: true,
    imports: [
        KnButton
    ],
    templateUrl: './header.component.html'
})
export class HeaderComponent {
    isUserActionsVisible = false;
    isNotificationVisible = false;
}
