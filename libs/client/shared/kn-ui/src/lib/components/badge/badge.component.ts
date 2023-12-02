import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'kn-badge',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './badge.component.html',
  styleUrls: ['./badge.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BadgeComponent {

  @Input({ required: true })
  content!: string;

  @Input()
  type: 'success' | 'warning' | 'info' | 'error' = 'info';

}
