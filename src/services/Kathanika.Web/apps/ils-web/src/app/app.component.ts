import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import {HeaderComponent} from "./components/header/header.component";
import {FooterComponent} from "./components/footer/footer.component";
import {SidebarComponent} from "./components/sidebar/sidebar.component";

@Component({
    standalone: true,
    imports: [RouterModule, HeaderComponent, FooterComponent, SidebarComponent],
    selector: 'kn-ils-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss',
})
export class AppComponent {
    title = 'ils-web';
}
