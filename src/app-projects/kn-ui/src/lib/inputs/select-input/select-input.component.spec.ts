import {ComponentFixture, TestBed} from '@angular/core/testing';

import {KnSelectInput} from './select-input.component';

describe('KnSelectInput', () => {
    let component: KnSelectInput;
    let fixture: ComponentFixture<KnSelectInput>;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [KnSelectInput],
        });
        fixture = TestBed.createComponent(KnSelectInput);
        component = fixture.componentInstance;
        //fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
