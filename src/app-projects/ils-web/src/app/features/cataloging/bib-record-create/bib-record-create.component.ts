import {Component, inject, signal, viewChild} from '@angular/core';
import {
    BookQuickAddGQL, BookQuickAddInput
} from "../../../graphql/generated/graphql-operations";
import {MessageAlertService} from "../../../core/message-alert/message-alert.service";
import {Router} from "@angular/router";
import {finalize} from "rxjs";
import {KnAlert, KnPanel} from "@kathanika/kn-ui";
import {BookRecordFormComponent} from "../book-record-form/book-record-form.component";

@Component({
    imports: [
        KnPanel,
        KnAlert,
        BookRecordFormComponent
    ],
    standalone: true,
    templateUrl: './bib-record-create.component.html'
})
export class BibRecordCreateComponent {
    private readonly gql = inject(BookQuickAddGQL);
    private readonly alertService = inject(MessageAlertService);
    private readonly router = inject(Router);

    readonly bookRecordForm = viewChild<BookRecordFormComponent>('bookRecordForm');

    isPanelLoading = signal(false);
    errors = signal<string[]>([]);

    onValidFormSubmit(formValue: BookQuickAddInput) {
        console.debug(formValue);
        this.isPanelLoading.set(true);
        this.gql.mutate({input: formValue})
            .pipe(finalize(() => {
                this.isPanelLoading.set(false);
            }))
            .subscribe({
                next: (result) => {
                    console.debug(result);
                    if (result.loading) {
                        this.isPanelLoading.set(true);
                        return;
                    }

                    if (result.errors || result.data?.bookQuickAdd.errors) {
                        const newErrors: string[] = [];
                        result.data?.bookQuickAdd.errors?.forEach((x) => {
                                if (x?.__typename === 'ValidationError') {
                                    newErrors.push(`${x.fieldName} - ${x.message}`);
                                } else {
                                    newErrors.push(x.message);
                                }
                            }
                        );
                        result.errors?.forEach((x) => newErrors.push(x.message));
                        this.errors.set(newErrors);
                    } else {
                        this.alertService.showToast(
                            'success',
                            result.data?.bookQuickAdd.message ?? 'Book added.',
                        );
                        this.bookRecordForm()?.resetForm();
                        this.router.navigate(['cataloging', 'bibs', result.data?.bookQuickAdd.data?.id]).then();
                    }
                },
                error: (err) => {
                    this.alertService.showHttpErrorPopup(JSON.stringify(err, null, 2));
                }
            });
    }

    closeAlert() {
        this.errors.set([]);
    }
}
