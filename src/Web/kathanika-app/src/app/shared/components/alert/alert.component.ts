import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  standalone: true,
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss'],
  imports: [CommonModule]
})
export class AlertComponent {
  @Input('closeable')
  closeable: boolean = false;

  @Output('onClosed')
  private onClosed = new EventEmitter<void>();

  close() {
    this.onClosed.emit();
  }
}
