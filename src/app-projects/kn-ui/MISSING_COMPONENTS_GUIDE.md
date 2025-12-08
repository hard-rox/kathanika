# Missing Components - Quick Reference Guide

This document provides implementation templates for the most critical missing components in the kn-ui library.

---

## 1. Modal/Dialog Component

### Priority: 🔴 Critical

### Implementation Template

```typescript
// modal.component.ts
import {Component, input, output, HostListener} from '@angular/core';
import {CommonModule} from '@angular/common';

@Component({
    selector: 'kn-modal',
    standalone: true,
    imports: [CommonModule],
    template: `
        @if (isOpen()) {
            <div class="modal-backdrop" (click)="onBackdropClick()">
                <div class="modal-container" 
                     [class]="'modal-' + size()"
                     (click)="$event.stopPropagation()"
                     role="dialog"
                     [attr.aria-modal]="true"
                     [attr.aria-labelledby]="title() ? 'modal-title' : null">
                    
                    @if (title()) {
                        <div class="modal-header">
                            <h2 id="modal-title">{{ title() }}</h2>
                            @if (dismissible()) {
                                <button 
                                    (click)="close()"
                                    aria-label="Close modal"
                                    class="modal-close-button">
                                    ×
                                </button>
                            }
                        </div>
                    }
                    
                    <div class="modal-body">
                        <ng-content></ng-content>
                    </div>
                    
                    @if (showFooter()) {
                        <div class="modal-footer">
                            <ng-content select="[footer]"></ng-content>
                        </div>
                    }
                </div>
            </div>
        }
    `,
    styles: [`
        .modal-backdrop {
            position: fixed;
            inset: 0;
            background: rgba(0, 0, 0, 0.5);
            display: flex;
            align-items: center;
            justify-content: center;
            z-index: 1000;
        }
        
        .modal-container {
            background: white;
            border-radius: 0.5rem;
            max-height: 90vh;
            overflow: auto;
        }
        
        .modal-sm { max-width: 400px; }
        .modal-md { max-width: 600px; }
        .modal-lg { max-width: 800px; }
        .modal-xl { max-width: 1200px; }
    `]
})
export class KnModal {
    readonly isOpen = input.required<boolean>();
    readonly title = input<string>();
    readonly size = input<'sm' | 'md' | 'lg' | 'xl'>('md');
    readonly dismissible = input(true);
    readonly showFooter = input(false);
    
    readonly closed = output<void>();
    
    @HostListener('document:keydown.escape')
    onEscapeKey() {
        if (this.dismissible() && this.isOpen()) {
            this.close();
        }
    }
    
    private onBackdropClick() {
        if (this.dismissible()) {
            this.close();
        }
    }
    
    close() {
        this.closed.emit();
    }
}
```

### Usage Example
```html
<kn-modal 
    [isOpen]="isModalOpen" 
    title="Confirm Action"
    size="md"
    [dismissible]="true"
    (closed)="isModalOpen = false">
    
    <p>Are you sure you want to proceed?</p>
    
    <div footer class="flex gap-2">
        <button kn-button variant="outline" (click)="isModalOpen = false">
            Cancel
        </button>
        <button kn-button color="primary" (click)="confirmAction()">
            Confirm
        </button>
    </div>
</kn-modal>
```

---

## 2. Toast Notification Service

### Priority: 🔴 Critical

### Implementation Template

```typescript
// toast.service.ts
import {Injectable, signal} from '@angular/core';

export interface Toast {
    id: string;
    message: string;
    type: 'success' | 'error' | 'warning' | 'info';
    duration: number;
}

@Injectable({providedIn: 'root'})
export class KnToastService {
    private toasts = signal<Toast[]>([]);
    
    readonly toasts$ = this.toasts.asReadonly();
    
    show(message: string, type: Toast['type'] = 'info', duration = 3000): string {
        const id = crypto.randomUUID();
        const toast: Toast = {id, message, type, duration};
        
        this.toasts.update(current => [...current, toast]);
        
        if (duration > 0) {
            setTimeout(() => this.dismiss(id), duration);
        }
        
        return id;
    }
    
    success(message: string, duration = 3000): string {
        return this.show(message, 'success', duration);
    }
    
    error(message: string, duration = 5000): string {
        return this.show(message, 'error', duration);
    }
    
    warning(message: string, duration = 4000): string {
        return this.show(message, 'warning', duration);
    }
    
    dismiss(id: string): void {
        this.toasts.update(current => current.filter(t => t.id !== id));
    }
}

// toast-container.component.ts
import {Component, inject} from '@angular/core';
import {CommonModule} from '@angular/common';
import {KnToastService} from './toast.service';

@Component({
    selector: 'kn-toast-container',
    standalone: true,
    imports: [CommonModule],
    template: `
        <div class="toast-container">
            @for (toast of toastService.toasts$(); track toast.id) {
                <div 
                    class="toast toast-{{ toast.type }}"
                    role="alert"
                    [attr.aria-live]="toast.type === 'error' ? 'assertive' : 'polite'">
                    <span>{{ toast.message }}</span>
                    <button 
                        (click)="toastService.dismiss(toast.id)"
                        aria-label="Dismiss">
                        ×
                    </button>
                </div>
            }
        </div>
    `,
    styles: [`
        .toast-container {
            position: fixed;
            top: 1rem;
            right: 1rem;
            z-index: 9999;
            display: flex;
            flex-direction: column;
            gap: 0.5rem;
        }
        
        .toast {
            padding: 1rem;
            border-radius: 0.5rem;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            display: flex;
            align-items: center;
            gap: 1rem;
            min-width: 300px;
            animation: slideIn 0.3s ease-out;
        }
        
        @keyframes slideIn {
            from { transform: translateX(100%); opacity: 0; }
            to { transform: translateX(0); opacity: 1; }
        }
    `]
})
export class KnToastContainer {
    readonly toastService = inject(KnToastService);
}
```

### Usage Example
```typescript
export class MyComponent {
    private toastService = inject(KnToastService);
    
    saveData() {
        this.api.save().subscribe({
            next: () => {
                this.toastService.success('Data saved successfully!');
            },
            error: (err) => {
                this.toastService.error('Failed to save data');
            }
        });
    }
}
```

---

## 3. Dropdown Menu Component

### Priority: 🔴 Critical

### Implementation Template

```typescript
// dropdown.component.ts
import {Component, input, output} from '@angular/core';
import {CommonModule} from '@angular/common';

export interface DropdownItem {
    label: string;
    value: any;
    icon?: string;
    disabled?: boolean;
    divider?: boolean;
}

@Component({
    selector: 'kn-dropdown',
    standalone: true,
    imports: [CommonModule],
    template: `
        <div class="dropdown" #dropdown>
            <button 
                class="dropdown-trigger"
                (click)="toggleDropdown()"
                [attr.aria-expanded]="isOpen"
                aria-haspopup="true">
                <ng-content select="[trigger]"></ng-content>
            </button>
            
            @if (isOpen) {
                <div class="dropdown-menu" role="menu">
                    @for (item of items(); track item.value) {
                        @if (item.divider) {
                            <div class="dropdown-divider"></div>
                        } @else {
                            <button 
                                class="dropdown-item"
                                [disabled]="item.disabled"
                                (click)="selectItem(item)"
                                role="menuitem">
                                @if (item.icon) {
                                    <span class="dropdown-item-icon">{{ item.icon }}</span>
                                }
                                <span>{{ item.label }}</span>
                            </button>
                        }
                    }
                </div>
            }
        </div>
    `
})
export class KnDropdown {
    readonly items = input.required<DropdownItem[]>();
    readonly placement = input<'bottom-start' | 'bottom-end'>('bottom-start');
    
    readonly itemSelected = output<DropdownItem>();
    
    isOpen = false;
    
    toggleDropdown() {
        this.isOpen = !this.isOpen;
    }
    
    selectItem(item: DropdownItem) {
        if (!item.disabled) {
            this.itemSelected.emit(item);
            this.isOpen = false;
        }
    }
}
```

---

## 4. Tabs Component

### Priority: 🟡 High

### Implementation Template

```typescript
// tabs.component.ts
import {Component, input, output} from '@angular/core';
import {CommonModule} from '@angular/common';

export interface Tab {
    id: string;
    label: string;
    icon?: string;
    disabled?: boolean;
}

@Component({
    selector: 'kn-tabs',
    standalone: true,
    imports: [CommonModule],
    template: `
        <div class="tabs-container">
            <div class="tabs-list" role="tablist">
                @for (tab of tabs(); track tab.id) {
                    <button 
                        class="tab-button"
                        [class.active]="activeTab() === tab.id"
                        [disabled]="tab.disabled"
                        (click)="selectTab(tab.id)"
                        role="tab"
                        [attr.aria-selected]="activeTab() === tab.id">
                        @if (tab.icon) {
                            <span class="tab-icon">{{ tab.icon }}</span>
                        }
                        {{ tab.label }}
                    </button>
                }
            </div>
            
            <div class="tab-content" role="tabpanel">
                <ng-content></ng-content>
            </div>
        </div>
    `
})
export class KnTabs {
    readonly tabs = input.required<Tab[]>();
    readonly activeTab = input<string>();
    
    readonly tabChanged = output<string>();
    
    selectTab(tabId: string) {
        this.tabChanged.emit(tabId);
    }
}
```

---

## 5. Tooltip Directive

### Priority: 🟡 High

### Implementation Template

```typescript
// tooltip.directive.ts
import {Directive, input, ElementRef, Renderer2, HostListener} from '@angular/core';

@Directive({
    selector: '[knTooltip]',
    standalone: true
})
export class KnTooltip {
    readonly knTooltip = input.required<string>();
    readonly position = input<'top' | 'bottom' | 'left' | 'right'>('top');
    
    private tooltipElement: HTMLElement | null = null;
    
    constructor(
        private el: ElementRef,
        private renderer: Renderer2
    ) {}
    
    @HostListener('mouseenter')
    onMouseEnter() {
        this.show();
    }
    
    @HostListener('mouseleave')
    onMouseLeave() {
        this.hide();
    }
    
    private show() {
        this.tooltipElement = this.renderer.createElement('div');
        this.renderer.addClass(this.tooltipElement, 'kn-tooltip');
        this.renderer.addClass(this.tooltipElement, `kn-tooltip-${this.position()}`);
        
        const text = this.renderer.createText(this.knTooltip());
        this.renderer.appendChild(this.tooltipElement, text);
        
        this.renderer.appendChild(document.body, this.tooltipElement);
        
        // Position calculation
        const hostPos = this.el.nativeElement.getBoundingClientRect();
        // ... positioning logic
    }
    
    private hide() {
        if (this.tooltipElement) {
            this.renderer.removeChild(document.body, this.tooltipElement);
            this.tooltipElement = null;
        }
    }
}
```

---

## 6. Card Component

### Priority: 🟡 High

### Implementation Template

```typescript
// card.component.ts
import {Component, input} from '@angular/core';
import {CommonModule} from '@angular/common';

@Component({
    selector: 'kn-card',
    standalone: true,
    imports: [CommonModule],
    template: `
        <div class="card" [class.hoverable]="hoverable()">
            @if (image()) {
                <img [src]="image()" alt="" class="card-image">
            }
            
            <div class="card-content">
                @if (title() || subtitle()) {
                    <div class="card-header">
                        @if (title()) {
                            <h3 class="card-title">{{ title() }}</h3>
                        }
                        @if (subtitle()) {
                            <p class="card-subtitle">{{ subtitle() }}</p>
                        }
                    </div>
                }
                
                <div class="card-body">
                    <ng-content></ng-content>
                </div>
                
                <div class="card-footer">
                    <ng-content select="[footer]"></ng-content>
                </div>
            </div>
        </div>
    `
})
export class KnCard {
    readonly title = input<string>();
    readonly subtitle = input<string>();
    readonly image = input<string>();
    readonly hoverable = input(false);
}
```

---

## 7. Skeleton Loader

### Priority: 🟡 Medium

### Implementation Template

```typescript
// skeleton.component.ts
import {Component, input} from '@angular/core';
import {CommonModule} from '@angular/common';

@Component({
    selector: 'kn-skeleton',
    standalone: true,
    imports: [CommonModule],
    template: `
        <div 
            class="skeleton"
            [class]="'skeleton-' + type()"
            [style.width]="width()"
            [style.height]="height()">
        </div>
    `,
    styles: [`
        .skeleton {
            background: linear-gradient(
                90deg,
                #f0f0f0 25%,
                #e0e0e0 50%,
                #f0f0f0 75%
            );
            background-size: 200% 100%;
            animation: loading 1.5s infinite;
        }
        
        .skeleton-text {
            height: 1rem;
            border-radius: 0.25rem;
        }
        
        .skeleton-circle {
            border-radius: 50%;
        }
        
        .skeleton-rectangle {
            border-radius: 0.25rem;
        }
        
        @keyframes loading {
            0% { background-position: 200% 0; }
            100% { background-position: -200% 0; }
        }
    `]
})
export class KnSkeleton {
    readonly type = input<'text' | 'circle' | 'rectangle'>('text');
    readonly width = input<string>();
    readonly height = input<string>();
}
```

---

## Implementation Priority Matrix

| Component | Priority | Complexity | Impact | Time Estimate |
|-----------|----------|------------|--------|---------------|
| Modal | 🔴 Critical | Medium | High | 1 week |
| Toast | 🔴 Critical | Low | High | 3 days |
| Dropdown | 🔴 Critical | Medium | High | 1 week |
| Tooltip | 🟡 High | Low | Medium | 2 days |
| Tabs | 🟡 High | Medium | High | 1 week |
| Card | 🟡 High | Low | Medium | 2 days |
| Skeleton | 🟡 Medium | Low | Low | 1 day |
| Breadcrumbs | 🟢 Medium | Low | Low | 2 days |

---

## Testing Checklist

For each new component:
- [ ] Unit tests with >80% coverage
- [ ] Accessibility testing (ARIA, keyboard nav)
- [ ] Responsive design testing
- [ ] Cross-browser testing
- [ ] Storybook stories (minimum 5 variants)
- [ ] Documentation with examples
- [ ] Integration tests
- [ ] Visual regression tests

---

**Note:** These templates provide a starting point. Each component should be thoroughly tested and refined based on actual usage patterns and feedback.
