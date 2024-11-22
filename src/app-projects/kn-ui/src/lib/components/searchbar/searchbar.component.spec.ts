import {ComponentFixture, TestBed} from '@angular/core/testing';

import {KnSearchbar} from './searchbar.component';

describe('SearchbarComponent', () => {
    let component: KnSearchbar<unknown>;
    let fixture: ComponentFixture<KnSearchbar<unknown>>;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [KnSearchbar],
        });
        fixture = TestBed.createComponent(KnSearchbar);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
