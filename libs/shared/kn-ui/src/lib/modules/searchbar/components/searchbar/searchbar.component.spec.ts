import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchbarComponent } from './searchbar.component';

describe('SearchbarComponent', () => {
  let component: SearchbarComponent<unknown>;
  let fixture: ComponentFixture<SearchbarComponent<unknown>>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SearchbarComponent],
    });
    fixture = TestBed.createComponent(SearchbarComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
