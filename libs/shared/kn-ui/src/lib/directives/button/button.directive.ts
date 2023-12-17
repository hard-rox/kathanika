import { Directive, ElementRef } from '@angular/core';

@Directive({
  selector: '[knButton]',
  standalone: true,
})
export class KnButton {
  constructor(el: ElementRef) {
    el.nativeElement.classList = 'bg-theme-gunmetal hover:bg-theme-rich-black active:bg-theme-rich-black px-4 py-2 text-white'
  }
}
