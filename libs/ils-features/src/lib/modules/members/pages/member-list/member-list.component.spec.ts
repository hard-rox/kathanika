import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberListComponent } from './member-list.component';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { GetMembersGQL, mockQueryGql } from '@kathanika/graphql-ts-client';
import { KnPagination, KnBadge } from '@kathanika/kn-ui';

describe('MemberListComponent', () => {
  let component: MemberListComponent;
  let fixture: ComponentFixture<MemberListComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberListComponent],
      imports: [KnPagination, RouterTestingModule, KnBadge],
      providers: [
        {
          provide: GetMembersGQL,
          useValue: mockQueryGql,
        },
        {
          provide: ActivatedRoute,
          useValue: {
            queryParams: of({
              size: 3,
              page: 2,
            }),
          },
        },
      ],
    });
    fixture = TestBed.createComponent(MemberListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
