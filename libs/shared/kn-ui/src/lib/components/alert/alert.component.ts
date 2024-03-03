import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  standalone: true,
  selector: 'kn-alert',
  templateUrl: './alert.component.html',
  imports: [CommonModule],
})
export class KnAlert {
  @Input()
  closeable = false;

  @Output()
  private closed = new EventEmitter<void>();

  protected close() {
    this.closed.emit();
  }
}
