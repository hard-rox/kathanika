import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'kn-dashboard-tile',
  templateUrl: './dashboard-tile.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DashboardTileComponent {}
