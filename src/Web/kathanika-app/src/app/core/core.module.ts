import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent, HeaderComponent, SidebarComponent } from '.';



@NgModule({
  declarations: [
    FooterComponent,
    HeaderComponent,
    SidebarComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    FooterComponent,
    HeaderComponent,
    SidebarComponent
  ]
})
export class CoreModule { }
