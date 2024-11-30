import {Directive, ElementRef, Input, OnChanges} from '@angular/core';

@Directive({
    selector: '[kn-button]',
    })
export class KnButton implements OnChanges {
    private readonly commonClasses = [
        'hover:bg-opacity-80',
        'disabled:text-gray-400',
        'disabled:pointer-events-none',
        'disabled:fill-gray-400',
        'inline-flex',
    ];
    private readonly roundedClasses = ['rounded-full', 'p-2'];
    private readonly rectanglePaddingClasses = ['px-4', 'py-2'];
    private readonly darkClasses = [
        'bg-theme-gunmetal',
        'hover:bg-theme-rich-black',
        'active:bg-theme-rich-black',
        'text-white',
        'fill-white',
    ];
    private readonly lightClasses = [
        'bg-gray-200',
        'hover:bg-theme-silver',
        'active:bg-theme-silver',
        'text-black',
    ];
    private readonly transparentClasses = [
        'bg-transparent',
        'hover:bg-theme-silver',
        'active:bg-theme-silver',
        'text-black',
    ];

    private readonly element!: HTMLElement;

    @Input() rounded = false;
    @Input() variant: 'light' | 'dark' | 'transparent' = 'dark';

    constructor(el: ElementRef) {
        this.element = el.nativeElement as HTMLElement;
        this.element.classList.add(...this.commonClasses);
        this.applyRoundedClasses();
        this.applyVariantClasses();
    }

    private removeRoundedClasses() {
        this.element.classList.remove(...this.rectanglePaddingClasses);
        this.element.classList.remove(...this.roundedClasses);
    }

    private removeVariantClasses() {
        this.element.classList.remove(...this.darkClasses);
        this.element.classList.remove(...this.lightClasses);
        this.element.classList.remove(...this.transparentClasses);
    }

    private applyRoundedClasses() {
        this.removeRoundedClasses();
        if (this.rounded) {
            this.element.classList.add(...this.roundedClasses);
            return;
        }
        this.element.classList.add(...this.rectanglePaddingClasses);
    }

    private applyVariantClasses() {
        this.removeVariantClasses();
        if (this.variant === 'light') {
            this.element.classList.add(...this.lightClasses);
            return;
        }
        if (this.variant == 'transparent') {
            this.element.classList.add(...this.transparentClasses);
            return;
        }
        this.element.classList.add(...this.darkClasses);
    }

    ngOnChanges(): void {
        this.applyRoundedClasses();
        this.applyVariantClasses();
    }
}
