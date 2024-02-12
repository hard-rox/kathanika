import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  standalone: true,
  selector: 'kn-panel',
  templateUrl: './panel.component.html',
  styleUrls: ['./panel.component.scss'],
  imports: [CommonModule],
})
export class KnPanel {
  @Input()
  panelTitle: string | null = null;

  @Input()
  isLoading = false;
}
