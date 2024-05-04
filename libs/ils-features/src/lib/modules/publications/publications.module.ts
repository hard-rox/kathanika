import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PublicationAddComponent } from './pages/publication-add/publication-add.component';
import { PublicationUpdateComponent } from './pages/publication-update/publication-update.component';
import { PublicationDetailsComponent } from './pages/publication-details/publication-details.component';
import { PublicationFormComponent } from './components/publication-form/publication-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { routes } from './publications.routes';
import {
  KnAlert,
  KnChip,
  KnDateInput,
  KnNumberInput,
  KnPagination,
  KnPanel,
  SearchbarModule,
  KnSelectInput,
  KnTextInput,
  KnTextareaInput
} from '@kathanika/kn-ui';
import { RecordPurchaseComponent } from './pages/record-purchase/record-purchase.component';

@NgModule({
  declarations: [
    PublicationListComponent,
    PublicationAddComponent,
    PublicationUpdateComponent,
    PublicationDetailsComponent,
    PublicationFormComponent,
    RecordPurchaseComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    KnPagination,
    KnTextInput,
    KnTextareaInput,
    KnDateInput,
    KnAlert,
    KnPanel,
    KnSelectInput,
    KnNumberInput,
    KnChip,
    SearchbarModule
  ],
})
export class PublicationsModule { }
