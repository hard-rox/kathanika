import { Component, HostBinding } from '@angular/core';

@Component({ template: '' })
export abstract class AbstractBlockComponent {
  @HostBinding('class') classes = 'block';
}
