import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss'],
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
