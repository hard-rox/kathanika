import { AcquirePublicationFormComponent } from "./acquire-publication-form.component";
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { SearchAuthorsGQL } from '@kathanika/graphql-ts-client';
import { KnSearchbarModule, KnChip } from '@kathanika/kn-ui';
import { mockQueryGql } from '../../../../test-utils/gql-test-utils';

describe('AcquirePublicationFormComponent', () => {
  let component: AcquirePublicationFormComponent;
  let fixture: ComponentFixture<AcquirePublicationFormComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AcquirePublicationFormComponent],
      imports: [ReactiveFormsModule, KnSearchbarModule, KnChip],
      providers: [
        {
          provide: SearchAuthorsGQL,
          useValue: mockQueryGql,
        },
      ],
    });
    fixture = TestBed.createComponent(AcquirePublicationFormComponent);
    component = fixture.componentInstance;
    //fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
