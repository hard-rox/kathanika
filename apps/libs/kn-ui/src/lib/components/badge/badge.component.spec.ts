import { ComponentFixture, TestBed } from '@angular/core/testing';

import { KnBadge } from './badge.component';

describe('KnBadge', () => {
    let component: KnBadge;
    let fixture: ComponentFixture<KnBadge>;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [KnBadge],
        });
        fixture = TestBed.createComponent(KnBadge);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
