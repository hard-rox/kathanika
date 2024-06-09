import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PublicationAcquireComponent } from './pages/publication-acquire/publication-acquire.component';
import { PublicationUpdateComponent } from './pages/publication-update/publication-update.component';
import { PublicationDetailsComponent } from './pages/publication-details/publication-details.component';
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
  KnSelectInput,
  KnTextInput,
  KnTextareaInput,
  KnSearchbar,
} from '@kathanika/kn-ui';
import { AcquirePublicationFormComponent } from './components/acquire-publication-form/acquire-publication-form.component';
import { PublicationPatchFormComponent } from './components/publication-patch-form/publication-patch-form.component';
import { PublicationAuthorsInputComponent } from './components/publication-authors-input/publication-authors-input.component';
import { PublicationPublisherInputComponent } from './components/publication-publisher-input/publication-publisher-input.component';

@NgModule({
  declarations: [
    PublicationListComponent,
    PublicationAcquireComponent,
    PublicationUpdateComponent,
    PublicationDetailsComponent,
    AcquirePublicationFormComponent,
    PublicationPatchFormComponent,
    PublicationAuthorsInputComponent,
    PublicationPublisherInputComponent,
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
    KnSearchbar,
  ],
})
export class PublicationsModule {}
