import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './header/header.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SidebarService } from './sidebar/sidebar.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HeaderComponent, SidebarComponent],
  template: `
    <!-- Header: sticky, full width, on top -->
    <app-header></app-header>
    
    <!-- Sidebar: sticky, collapsible -->
    <app-sidebar></app-sidebar>
    
    <!-- Main content area -->
    <main 
      class="min-h-screen bg-gray-50 transition-all duration-300 ease-in-out"
      [class.lg:ml-64]="!isCollapsed()"
      [class.lg:ml-16]="isCollapsed()"
      [class.pt-16]="true">
      
      <!-- Main content container with max width and responsive padding -->
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-6">
        <router-outlet></router-outlet>
      </div>
    </main>
  `
})
export class App {
  private sidebarService = inject(SidebarService);
  protected readonly isCollapsed = this.sidebarService.isCollapsed;
}
