import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeRoutingModule } from './home-routing.module';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { DashboardTileComponent } from './components/dashboard-tile/dashboard-tile.component';


@NgModule({
  declarations: [
    DashboardComponent,
    DashboardTileComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule
  ]
})
export class HomeModule { }
