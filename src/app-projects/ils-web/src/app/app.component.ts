import {Component} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {KnFileInput} from "@kathanika/kn-ui";

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet],
    template: `<!--<kathanika-header></kathanika-header>-->
    <!--    <kathanika-sidebar></kathanika-sidebar>-->
    <main class="pl-72 py-20 pr-4">
        <router-outlet></router-outlet>
    </main>
<!--    <kn-file-input></kn-file-input>-->
    <!--<kathanika-footer></kathanika-footer>-->`
})
export class AppComponent {
    title = 'ils-web';
}
