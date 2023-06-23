import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss'],
})
export class AlertComponent {
  @Output('onClosed')
  private onClosed = new EventEmitter<void>();

  close() {
    this.onClosed.emit();
  }
}
