import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {HeaderComponent} from "./header/header.component";
import {SidebarComponent} from "./sidebar/sidebar.component";
import {FooterComponent} from "./footer/footer.component";

@Component({
    selector: 'app-root',
    imports: [RouterOutlet, HeaderComponent, SidebarComponent, FooterComponent],
    template: `
        <app-header></app-header>
        <app-sidebar></app-sidebar>
        <main class="pl-72 py-20 pr-4">
            <router-outlet></router-outlet>
        </main>
        <app-footer></app-footer>
    `
})
export class AppComponent {
    title = 'ils-web';
}
