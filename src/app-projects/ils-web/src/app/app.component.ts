import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { GetVendorsGQL } from '@kathanika/graphql-client';
import { KnButton, KnPanel } from '@kathanika/kn-ui';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [KnButton, KnPanel],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  constructor(
    // private gql: GetVendorsGQL
  ) { }
  title = 'ils-web';
}
