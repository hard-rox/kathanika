import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  standalone: true,
  selector: 'kn-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss'],
  imports: [CommonModule]
})
export class AlertComponent {
  @Input()
  closeable: boolean = false;

  @Output()
  private closed = new EventEmitter<void>();

  close() {
    this.closed.emit();
  }
}
