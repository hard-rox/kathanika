import {Directive, ElementRef, inject, input, effect} from '@angular/core';
import {Color} from "../../types/color";

@Directive({
    selector: '[kn-button]',
})
export class KnButton {
    private readonly commonClasses = [
        'disabled:text-gray-400',
        'disabled:pointer-events-none',
        'disabled:fill-gray-400',
        'inline-flex',
        'rounded-sm',
        'items-center',
        'justify-center',
    ];
    private readonly roundedClasses = ['rounded-full', 'p-2'];
    private readonly rectanglePaddingClasses = ['px-2', 'py-2'];
    private readonly outlineClasses = [
        'bg-transparent',
        'border-solid',
        'border-2',
        'hover:bg-black/20',
        'active:bg-black/20',
    ]

    private readonly transparentClasses = [
        'bg-transparent',
        'border-none',
        'hover:bg-black/20',
        'active:bg-black/20',
    ]

    private readonly solidClasses = [
    ]

    private readonly primaryClasses = [
        'bg-theme-gunmetal',
        'hover:bg-theme-rich-black',
        'active:bg-theme-rich-black',
        'border-theme-gunmetal',
        'hover:border-theme-rich-black',
        'active:border-theme-rich-black',
        'text-white',
        'fill-white',
    ];
    private readonly secondaryClasses = [
        'bg-theme-davys-gray',
        'hover:bg-theme-gunmetal',
        'active:bg-theme-gunmetal',
        'border-theme-davys-gray',
        'hover:border-theme-gunmetal',
        'active:border-theme-gunmetal',
        'text-white',
        'fill-white',
    ];
    private readonly infoClasses = [
        'bg-gray-200',
        'hover:bg-theme-silver',
        'active:bg-theme-silver',
        'border-gray-200',
        'hover:border-theme-silver',
        'active:border-theme-silver',
        'text-black',
        'fill-black'
    ];
    private readonly successClasses = [
        'bg-theme-office-green/90',
        'hover:bg-theme-office-green/100',
        'active:bg-theme-office-green/100',
        'border-theme-office-green/90',
        'hover:border-theme-office-green/100',
        'active:border-theme-office-green/100',
        'text-white',
        'fill-white'
    ];
    private readonly warningClasses = [
        'bg-theme-spanish-orange/90',
        'hover:bg-theme-spanish-orange/100',
        'active:bg-theme-spanish-orange/100',
        'border-theme-spanish-orange/90',
        'hover:border-theme-spanish-orange/100',
        'active:border-theme-spanish-orange/100',
        'text-white',
        'fill-black'
    ]
    private readonly errorClasses = [
        'bg-theme-fire-red/90',
        'hover:bg-theme-fire-red/100',
        'active:bg-theme-fire-red/100',
        'border-theme-fire-red/90',
        'hover:border-theme-fire-red/100',
        'active:border-theme-fire-red/100',
        'text-white',
        'fill-white'
    ]

    private readonly element!: HTMLElement;

    readonly rounded = input(false);
    readonly color = input<Color>('primary');
    readonly variant = input<Variant>('solid');

    constructor() {
        const el = inject(ElementRef);

        this.element = el.nativeElement as HTMLElement;
        this.element.classList.add(...this.commonClasses);
        this.applyRoundedClasses(); //Use padding name
        // this.applyVariantClasses();
        // this.applyColorClasses();

        effect(() => {
            this.applyColorClasses(this.color());
            this.applyVariantClasses(this.variant());
        });
    }
    

    private applyVariantClasses(variant: Variant) {
        const currentClasses = this.element.className.split(' ');
        const removableClasses = currentClasses
            .filter(x => x.startsWith('text-') || x.startsWith('fill-'));
        this.element.classList.remove(...removableClasses);
        
        switch (variant) {
            case 'outline':
                this.element.classList.add(...this.outlineClasses);
                break;
            case 'transparent':
                this.element.classList.add(...this.transparentClasses);
                break;
            case 'solid':
                this.element.classList.add(...this.solidClasses);
                break;
        }
    }

    private applyColorClasses(color: Color) {
        switch (color) {
            case 'primary':
                this.element.classList.add(...this.primaryClasses);
                break;
            case 'secondary':
                this.element.classList.add(...this.secondaryClasses);
                break;
            case "info":
                this.element.classList.add(...this.infoClasses);
                break;
            case "success":
                this.element.classList.add(...this.successClasses);
                break;
            case "warning":
                this.element.classList.add(...this.warningClasses);
                break;
            case "error":
                this.element.classList.add(...this.errorClasses);
                break;
        }
    }

    private removeRoundedClasses() {
        this.element.classList.remove(...this.rectanglePaddingClasses);
        this.element.classList.remove(...this.roundedClasses);
    }

    private removeVariantClasses() {
        // this.element.classList.remove(...this.primaryClasses);
        // this.element.classList.remove(...this.infoClasses);
        // this.element.classList.remove(...this.transparentClasses);
    }

    private applyRoundedClasses() {
        this.removeRoundedClasses();
        if (this.rounded()) {
            this.element.classList.add(...this.roundedClasses);
            return;
        }
        this.element.classList.add(...this.rectanglePaddingClasses);
    }
}

type Variant = 'transparent' | 'outline' | 'solid';