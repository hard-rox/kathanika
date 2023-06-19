import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorUpdateComponent } from './author-update.component';
import { ActivatedRoute } from '@angular/router';
import {
  GetAuthorGQL,
  UpadateAuthorGQL,
} from 'src/app/graphql/generated/graphql-operations';
import { mockMutatuionGql, mockQueryGql } from 'src/test-utils/gql-test-utils';
import { AuthorFormComponent } from '../../components/author-form/author-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PanelComponent } from 'src/app/shared/modules/panel/components/panel/panel.component';

describe('AuthorUpdateComponent', () => {
  const routeParam = '12345';
  let component: AuthorUpdateComponent;
  let fixture: ComponentFixture<AuthorUpdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule],
      declarations: [
        AuthorFormComponent,
        AuthorUpdateComponent,
        PanelComponent,
      ],
      providers: [
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: {
                id: routeParam,
              },
            },
          },
        },
        {
          provide: GetAuthorGQL,
          useValue: mockQueryGql,
        },
        {
          provide: UpadateAuthorGQL,
          useValue: mockMutatuionGql,
        },
      ],
    });
    fixture = TestBed.createComponent(AuthorUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should fetch with valid route param id in ngOnInit', () => {
    const gqlSpy = spyOn<any>(component['gql'], 'fetch').and.returnValue({
      subscribe: () => {},
    });
    component.ngOnInit();
    expect(gqlSpy).toHaveBeenCalledOnceWith({ id: routeParam });
  });

  xit('should call "showToast" on onValidFormSubmit when no errors', () => {
    const alertServiceSpy = spyOn(component['alertService'], 'showToast');
    spyOn<any>(component['mutation'], 'mutate').and.returnValue({
      subscribe: () => {}
    });
    component.onValidFormSubmit({ firstName: '', lastName: '', dateOfBirth: '', dateOfDeath: null, nationality: '', biography: '' });
    expect(alertServiceSpy).toHaveBeenCalled();
  });
});
