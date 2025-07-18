import {ComponentFixture, TestBed} from '@angular/core/testing';
import {NgControl} from '@angular/forms';

import {KnTextareaInput} from './textarea-input.component';

describe('KnTextareaInput', () => {
    let component: KnTextareaInput;
    let fixture: ComponentFixture<KnTextareaInput>;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [KnTextareaInput],
            providers: [
                {
                    provide: NgControl,
                    useValue: {}
                }
            ]
        });
        fixture = TestBed.createComponent(KnTextareaInput);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
