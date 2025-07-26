import {Component, inject, OnInit} from '@angular/core';
import {CommonModule} from "@angular/common";
import {KnButton, KnPagination, KnPanel, KnSelectInput} from "@kathanika/kn-ui";
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
        KnPanel
    ],
    standalone: true,
    templateUrl: './bib-record-list.component.html'
})
export class BibRecordListComponent
    extends BasePaginatedListComponent<BibRecordListQuery, BibRecordListQueryVariables>
    implements OnInit {
    protected setSearchTextQueryFilter(searchText: string): void {
        if (!searchText || searchText.length == 0) {
            this.queryVariables.filter = null;
            return;
        }
        this.queryVariables.filter = {
            or: [
                {
                    titleStatement: {
                        title: {contains: searchText}
                    }
                },
                {
                    titleStatement: {
                        statementOfResponsibility: {contains: searchText}
                    }
                }
            ]
        };
    }

    constructor() {
        const gql = inject(BibRecordListGQL);
        const activatedRoute = inject(ActivatedRoute);
        const router = inject(Router);
        super(gql, activatedRoute, router, {
            skip: 0,
            take: 20,
            sortBy: {
                titleStatement: {
                    title: SortEnumType.Asc
                }
            }
        });
    }

    ngOnInit(): void {
        this.init();
    }
}
