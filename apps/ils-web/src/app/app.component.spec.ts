import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { FooterComponent } from './components/footer/footer.component';

describe('AppComponent', () => {
    let fixture: ComponentFixture<AppComponent>;
    let nativeElement: HTMLElement;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            imports: [
                AppComponent,
                HeaderComponent,
                FooterComponent,
                SidebarComponent,
            ],
        }).compileComponents();

        fixture = TestBed.createComponent(AppComponent);
        nativeElement = fixture.nativeElement as HTMLElement;
    });

    it('should create the app', () => {
        const app = fixture.componentInstance;
        expect(app).toBeTruthy();
    });

    it('should render header', () => {
        expect(nativeElement.querySelector('kn-ils-web-header')).toBeTruthy();
    });

    it('should render sidebar', () => {
        expect(nativeElement.querySelector('kn-ils-web-sidebar')).toBeTruthy();
    });

    it('should render footer', () => {
        expect(nativeElement.querySelector('kn-ils-web-footer')).toBeTruthy();
    });

    it('should have router outlet', () => {
        expect(nativeElement.querySelector('router-outlet')).toBeTruthy();
    });
});
