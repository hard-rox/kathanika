import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AbstractBlockComponent } from '../../abstractions/abstract-block-component';

@Component({
  standalone: true,
  selector: 'kn-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss'],
  imports: [CommonModule],
})
export class KnAlert extends AbstractBlockComponent {
  @Input()
  closeable = false;

  @Output()
  private closed = new EventEmitter<void>();

  protected close() {
    this.closed.emit();
  }
}
