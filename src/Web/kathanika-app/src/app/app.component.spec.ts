import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { HeaderComponent } from './components/header/header.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { FooterComponent } from './components/footer/footer.component';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let nativeElement: HTMLElement;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        AppComponent,
        HeaderComponent,
        FooterComponent,
        SidebarComponent
      ],
    }).compileComponents();

    component = TestBed.createComponent(AppComponent);
    fixture = TestBed.createComponent(AppComponent);
    nativeElement = fixture.nativeElement as HTMLElement;
  });

  beforeEach(() => {

  })

  it('should create the app', () => {
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it('should render header', () => {
    expect(nativeElement.querySelector('app-header')).toBeTruthy();
  });

  it('should render sidebar', () => {
    expect(nativeElement.querySelector('app-sidebar')).toBeTruthy();
  });

  it('should render footer', () => {
    expect(nativeElement.querySelector('app-footer')).toBeTruthy();
  });

  it('should have router outlet', () => {
    expect(nativeElement.querySelector('router-outlet')).toBeTruthy();
  });
});
