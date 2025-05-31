import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {BaseQueryComponent} from '../../../abstractions/base-query-component';
import {KnButton, KnPanel} from '@kathanika/kn-ui';
import {
    BibRecordDetailsQuery,
    BibRecordDetailsQueryVariables,
    BibRecordDetailsGQL
} from '../../../graphql/generated/graphql-operations';

@Component({
    selector: 'app-bib-record-details',
    templateUrl: './bib-record-details.component.html',
    standalone: true,
    imports: [
        CommonModule,
        KnPanel,
        KnButton,
        RouterLink
    ],
})
export class BibRecordDetailsComponent
    extends BaseQueryComponent<BibRecordDetailsQuery, BibRecordDetailsQueryVariables>
    implements OnInit {
    private readonly activatedRoute = inject(ActivatedRoute);

    constructor() {
        const gql = inject(BibRecordDetailsGQL);
        super(gql);
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
