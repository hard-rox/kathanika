import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublishersRoutingModule } from './publishers-routing.module';
import { PublisherFormComponent } from './components/publisher-form/publisher-form.component';
import { TextInputComponent } from 'src/app/shared/components/text-input/text-input.component';
import { TextareaInputComponent } from 'src/app/shared/components/textarea-input/textarea-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PublisherListComponent } from './pages/publisher-list/publisher-list.component';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { PublisherAddComponent } from './pages/publisher-add/publisher-add.component';
import { PublisherUpdateComponent } from './pages/publisher-update/publisher-update.component';
import { PanelComponent } from 'src/app/shared/components/panel/panel.component';
import { AlertComponent } from 'src/app/shared/components/alert/alert.component';


@NgModule({
  declarations: [
    PublisherFormComponent,
    PublisherListComponent,
    PublisherAddComponent,
    PublisherUpdateComponent
  ],
  imports: [
    CommonModule,
    PublishersRoutingModule,
    ReactiveFormsModule,
    PaginationComponent,
    TextInputComponent,
    TextareaInputComponent,
    PanelComponent,
    AlertComponent
  ]
})
export class PublishersModule { }
