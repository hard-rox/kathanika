import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchbarComponent } from './searchbar.component';

describe('SearchbarComponent', () => {
  let component: SearchbarComponent<any>;
  let fixture: ComponentFixture<SearchbarComponent<any>>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SearchbarComponent]
    });
    fixture = TestBed.createComponent(SearchbarComponent);
    component = fixture.componentInstance;
    // fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
