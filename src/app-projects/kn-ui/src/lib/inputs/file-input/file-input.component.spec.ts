import {ComponentFixture, TestBed} from '@angular/core/testing';
import {NgControl} from '@angular/forms';
import {KnFileInput} from './file-input.component';

describe('FileInputComponent', () => {
    let component: KnFileInput;
    let fixture: ComponentFixture<KnFileInput>;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [KnFileInput],
            providers: [
                {
                    provide: NgControl,
                    useValue: {}
                }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(KnFileInput);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it.skip('should create', () => {
        expect(component).toBeTruthy();
    });
});
