import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchbarResultComponent } from './searchbar-result.component';
import { SearchbarComponent } from '../searchbar/searchbar.component';

describe('SearchbarResultComponent', () => {
  let component: SearchbarResultComponent<unknown>;
  let fixture: ComponentFixture<SearchbarResultComponent<unknown>>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [SearchbarComponent, SearchbarResultComponent],
    });
    fixture = TestBed.createComponent(SearchbarResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  xit('should create', () => {
    expect(component).toBeTruthy();
  });
});
