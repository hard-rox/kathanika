import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'kn-badge',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './badge.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class KnBadge {
  @Input({ required: true })
  content!: string;

  @Input()
  type: 'success' | 'warning' | 'info' | 'error' = 'info';
}
