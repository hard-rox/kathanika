import { ChangeDetectionStrategy, Component, Input } from '@angular/core';

@Component({
  selector: 'app-panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.scss'],
})
export class PanelComponent {
  @Input('panelTitle')
  panelTitle: string | null = null;

  @Input('isLoading')
  isLoading: boolean = false;
}
