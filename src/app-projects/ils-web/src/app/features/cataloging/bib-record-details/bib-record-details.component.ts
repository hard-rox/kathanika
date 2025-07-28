import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ActivatedRoute} from '@angular/router';
import {BaseQueryComponent} from '../../../abstractions/base-query-component';
import {KnBadge, KnButton, KnPanel} from '@kathanika/kn-ui';
import {
    BibRecordDetailsQuery,
    BibRecordDetailsQueryVariables,
    BibRecordDetailsGQL, ItemStatus
} from '../../../graphql/generated/graphql-operations';

@Component({
    selector: 'app-bib-record-details',
    templateUrl: './bib-record-details.component.html',
    standalone: true,
    imports: [
        CommonModule,
        KnPanel,
        KnButton,
        KnBadge,
    ],
})
export class BibRecordDetailsComponent
    extends BaseQueryComponent<BibRecordDetailsQuery, BibRecordDetailsQueryVariables>
    implements OnInit {
    private readonly activatedRoute = inject(ActivatedRoute);

    protected readonly bibItemStatus = ItemStatus;

    constructor() {
        const gql = inject(BibRecordDetailsGQL);
        super(gql);
    }

    protected itemsCountByStatus: Record<ItemStatus, number> = Object.values(ItemStatus)
        .reduce((acc, status) => ({
            ...acc,
            [status]: 0
        }), {} as Record<ItemStatus, number>);

    ngOnInit(): void {
        const bibRecordId = this.activatedRoute.snapshot.params['id'];
        if (bibRecordId && bibRecordId.length > 0) {
            this.queryVariables = {
                id: bibRecordId,
            };
            this.queryRef.refetch(this.queryVariables)
                .then(result => {
                    const bibItems = result.data?.bibRecord?.bibItems;

                    // Reset item counts
                    Object.keys(this.itemsCountByStatus).forEach(status => {
                        this.itemsCountByStatus[status as ItemStatus] = 0;
                    });

                    // Count items by status
                    bibItems?.forEach(item => {
                        if (item?.status) {
                            this.itemsCountByStatus[item.status] = (this.itemsCountByStatus[item.status] || 0) + 1;
                        }
                    });
                })
        }
    }
}