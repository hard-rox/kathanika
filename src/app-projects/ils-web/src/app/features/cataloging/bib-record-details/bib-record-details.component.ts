import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ActivatedRoute} from '@angular/router';
import {BaseQueryComponent} from '../../../abstractions/base-query-component';
import {KnBadge, KnButton, KnPanel} from '@kathanika/kn-ui';
import {
    BibRecordDetailsQuery,
    BibRecordDetailsQueryVariables,
    BibRecordDetailsGQL, ItemStatus, BibItem
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

    protected itemCountByStatus(items: ({status: ItemStatus} | null)[] | undefined | null, status: ItemStatus): number {
        console.log('called');
        return items?.filter(item => item?.status === status).length ?? 0;
    }

    ngOnInit(): void {
        const bibRecordId = this.activatedRoute.snapshot.params['id'];
        if (bibRecordId && bibRecordId.length > 0) {
            this.queryVariables = {
                id: bibRecordId,
            };
            this.queryRef.refetch(this.queryVariables);
        }
    }
}