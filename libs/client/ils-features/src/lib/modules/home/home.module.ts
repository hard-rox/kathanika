import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { DashboardTileComponent } from './components/dashboard-tile/dashboard-tile.component';
import { RouterModule } from '@angular/router';
import { routes } from './home.routes';

@NgModule({
  declarations: [DashboardComponent, DashboardTileComponent],
  imports: [CommonModule, RouterModule.forChild(routes)],
})
export class HomeModule {}
