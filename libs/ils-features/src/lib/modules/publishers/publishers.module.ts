import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublisherFormComponent } from './components/publisher-form/publisher-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PublisherListComponent } from './pages/publisher-list/publisher-list.component';
import { PublisherAddComponent } from './pages/publisher-add/publisher-add.component';
import { PublisherUpdateComponent } from './pages/publisher-update/publisher-update.component';
import { RouterModule } from '@angular/router';
import { routes } from './publishers.routes';
import {
  KnAlert,
  KnPagination,
  KnPanel,
  KnTextInput,
  KnTextareaInput,
} from '@kathanika/kn-ui';

@NgModule({
  declarations: [
    PublisherFormComponent,
    PublisherListComponent,
    PublisherAddComponent,
    PublisherUpdateComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    KnPagination,
    KnTextInput,
    KnTextareaInput,
    KnPanel,
    KnAlert,
  ],
})
export class PublishersModule {}
