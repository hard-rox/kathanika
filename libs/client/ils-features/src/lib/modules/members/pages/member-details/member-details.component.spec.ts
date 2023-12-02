import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MemberDetailsComponent } from './member-details.component';
import { ActivatedRoute } from '@angular/router';
import { GetMemberGQL } from '@kathanika/graphql-consumer';
import { BadgeComponent, PanelComponent } from '@kathanika/kn-ui';
import { mockQueryGql } from '../../../../test-utils/gql-test-utils';

describe('MemberDetailsComponent', () => {
  let component: MemberDetailsComponent;
  let fixture: ComponentFixture<MemberDetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MemberDetailsComponent],
      imports: [
        BadgeComponent,
        PanelComponent
      ],
      providers: [
        {
          provide: GetMemberGQL,
          useValue: mockQueryGql
        },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              params: {
                id: 'mockMemberId',
              },
            },
          },
        },
      ]
    });
    fixture = TestBed.createComponent(MemberDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
