import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { NxWelcomeComponent } from './nx-welcome.component';
import { KnUiComponent } from '@kathanika/kn-ui';

@Component({
  standalone: true,
  imports: [NxWelcomeComponent, RouterModule, KnUiComponent],
  selector: 'kathanika-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent {
  title = 'kathanika-ils';
}
