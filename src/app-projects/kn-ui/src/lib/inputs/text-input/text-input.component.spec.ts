import {ComponentFixture, TestBed} from '@angular/core/testing';
import {NgControl} from '@angular/forms';

import {KnTextInput} from './text-input.component';

describe('KnTextInput', () => {
    let component: KnTextInput;
    let fixture: ComponentFixture<KnTextInput>;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [KnTextInput],
            providers: [
                {
                    provide: NgControl,
                    useValue: {}
                }
            ]
        });
        fixture = TestBed.createComponent(KnTextInput);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
