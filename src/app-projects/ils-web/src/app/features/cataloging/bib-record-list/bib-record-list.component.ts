import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from "@angular/common";
import {KnButton, KnChip, KnPagination, KnPanel} from "@kathanika/kn-ui";
import {ActivatedRoute, Router, RouterLink} from "@angular/router";
import {BasePaginatedListComponent} from "../../../abstractions/base-paginated-list-component";
import {
    BibRecordListGQL,
    BibRecordListQuery,
    BibRecordListQueryVariables, SortEnumType
} from "../../../graphql/generated/graphql-operations";

@Component({
    imports: [
        CommonModule,
        KnButton,
        KnPagination,
        RouterLink,
        KnPanel,
        KnChip
    ],
    standalone: true,
    templateUrl: './bib-record-list.component.html'
})
export class BibRecordListComponent
    extends BasePaginatedListComponent<BibRecordListQuery, BibRecordListQueryVariables>
    implements OnInit {
    protected override setSearchTextQueryFilter(searchText: string): void {
        if (!searchText || searchText.length == 0) {
            this.queryVariables.filter = null;
            return;
        }
        this.queryVariables.filter = {
            or: []
        };
    }

    constructor() {
        const gql = inject(BibRecordListGQL);
        const activatedRoute = inject(ActivatedRoute);
        const router = inject(Router);
        super(gql, activatedRoute, router, {
            skip: 0,
            take: 20,
        });
    }

    ngOnInit(): void {
        this.init();
    }
}
