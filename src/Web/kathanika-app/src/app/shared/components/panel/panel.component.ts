import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.scss'],
  imports: [CommonModule]
})
export class PanelComponent {
  @Input('panelTitle')
  panelTitle: string | null = null;

  @Input('isLoading')
  isLoading: boolean = false;
}
