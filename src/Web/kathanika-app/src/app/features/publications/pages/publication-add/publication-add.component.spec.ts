import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicationAddComponent } from './publication-add.component';
import { PublicationFormComponent } from '../../components/publication-form/publication-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { AddPublicationGQL } from 'src/app/graphql/generated/graphql-operations';
import { mockMutationGql } from 'src/test-utils/gql-test-utils';
import { PanelComponent } from 'src/app/shared/components/panel/panel.component';
import { AlertComponent } from 'src/app/shared/components/alert/alert.component';

describe('PublicationAddComponent', () => {
  let component: PublicationAddComponent;
  let fixture: ComponentFixture<PublicationAddComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PublicationAddComponent, PublicationFormComponent],
      imports: [ReactiveFormsModule, PanelComponent, AlertComponent],
      providers: [
        {
          provide: AddPublicationGQL,
          useValue: mockMutationGql
        }
      ]
    });
    fixture = TestBed.createComponent(PublicationAddComponent);
    component = fixture.componentInstance;
    //fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
