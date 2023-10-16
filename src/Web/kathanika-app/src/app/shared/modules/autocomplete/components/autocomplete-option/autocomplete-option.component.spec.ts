import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AutocompleteOptionComponent } from './autocomplete-option.component';

describe('AutocompleteOptionComponent', () => {
  let component: AutocompleteOptionComponent;
  let fixture: ComponentFixture<AutocompleteOptionComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AutocompleteOptionComponent]
    });
    fixture = TestBed.createComponent(AutocompleteOptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
