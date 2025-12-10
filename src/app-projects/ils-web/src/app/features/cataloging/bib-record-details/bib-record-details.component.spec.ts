import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { BibRecordDetailsComponent } from './bib-record-details.component';
import { BibRecordDetailsGQL, ItemStatus } from '../../../graphql/generated/graphql-operations';
import { ApolloTestingModule } from 'apollo-angular/testing';
import { KnBadge, KnButton, KnPanel } from '@kathanika/kn-ui';
import { Mocked } from 'vitest';

describe.skip('BibRecordDetailsComponent', () => {
    let component: BibRecordDetailsComponent;
    let fixture: ComponentFixture<BibRecordDetailsComponent>;
    let bibRecordDetailsGQLMock: Mocked<BibRecordDetailsGQL>;

    beforeEach(async () => {
        // Create mock for BibRecordDetailsGQL
        bibRecordDetailsGQLMock = {} as Mocked<BibRecordDetailsGQL>;

        await TestBed.configureTestingModule({
            imports: [
                BibRecordDetailsComponent,
                ApolloTestingModule,
                KnPanel,
                KnBadge,
                KnButton
            ],
            providers: [
                {
                    provide: ActivatedRoute,
                    useValue: {
                        snapshot: {
                            params: { id: 'BIB123' }
                        }
                    }
                },
                {
                    provide: BibRecordDetailsGQL,
                    useValue: bibRecordDetailsGQLMock
                }
            ]
        }).compileComponents();

        fixture = TestBed.createComponent(BibRecordDetailsComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should load bib record on init', () => {
        expect(component['queryVariables']).toEqual({ id: 'BIB123' });
        expect(bibRecordDetailsGQLMock.watch).toHaveBeenCalled();
    });

    it('should update itemsCountByStatus after data is fetched', () => {
        fixture.detectChanges();

        // Verify the item counts were updated correctly based on the mock data
        expect(component['itemsCountByStatus'][ItemStatus.Available]).toBe(2);
        expect(component['itemsCountByStatus'][ItemStatus.CheckedOut]).toBe(1);
    });
});
