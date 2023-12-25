import { Directive, ElementRef, Input, OnChanges } from '@angular/core';

@Directive({
  selector: '[kn-button]',
  standalone: true,
})
export class KnButton implements OnChanges{
  private commonClasses = ['hover:bg-opacity-80', 'disabled:text-gray-400', 'disabled:pointer-events-none', 'disabled:fill-gray-400'];
  private roundedClasses = ['rounded-full', 'p-2'];
  private rectanglePaddingClasses = ['px-4', 'py-2'];
  private darkClasses = ['bg-theme-gunmetal', 'hover:bg-theme-rich-black', 'active:bg-theme-rich-black', 'text-white'];
  private lightClasses = ['bg-gray-200', 'hover:bg-theme-silver', 'active:bg-theme-silver', 'text-black'];

  private element!: HTMLElement;

  @Input() rounded = false;
  @Input() variant: 'light' | 'dark' = 'dark';

  constructor(el: ElementRef) {
    this.element = el.nativeElement as HTMLElement;
    this.element.classList.add(...this.commonClasses);
    this.applyClasses();
  }

  private applyClasses() {
    if (this.rounded) {
      this.element.classList.remove(...this.rectanglePaddingClasses);
      this.element.classList.add(...this.roundedClasses);
    } else {
      this.element.classList.remove(...this.roundedClasses);
      this.element.classList.add(...this.rectanglePaddingClasses);
    }

    if (this.variant === 'light') {
      this.element.classList.remove(...this.darkClasses);
      this.element.classList.add(...this.lightClasses);
    } else {
      this.element.classList.remove(...this.lightClasses);
      this.element.classList.add(...this.darkClasses);
    }
  }

  ngOnChanges(): void {
    this.applyClasses();
  }
}
