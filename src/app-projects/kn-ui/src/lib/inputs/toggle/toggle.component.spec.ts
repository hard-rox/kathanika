import {ComponentFixture, TestBed} from '@angular/core/testing';
import {NgControl} from '@angular/forms';

import {KnToggle} from './toggle.component';

describe('KnToggle', () => {
    let component: KnToggle;
    let fixture: ComponentFixture<KnToggle>;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [KnToggle],
            providers: [
                {
                    provide: NgControl,
                    useValue: {}
                }
            ]
        });
        fixture = TestBed.createComponent(KnToggle);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
