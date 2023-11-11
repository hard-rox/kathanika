import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PublicationListComponent } from './pages/publication-list/publication-list.component';
import { PublicationAddComponent } from './pages/publication-add/publication-add.component';
import { PublicationUpdateComponent } from './pages/publication-update/publication-update.component';
import { PublicationDetailsComponent } from './pages/publication-details/publication-details.component';
import { PublicationFormComponent } from './components/publication-form/publication-form.component';
import { ReactiveFormsModule } from '@angular/forms';
import { PaginationComponent } from 'src/app/shared/components/pagination/pagination.component';
import { TextInputComponent } from 'src/app/shared/components/text-input/text-input.component';
import { TextareaInputComponent } from 'src/app/shared/components/textarea-input/textarea-input.component';
import { DateInputComponent } from 'src/app/shared/components/date-input/date-input.component';
import { AlertComponent } from 'src/app/shared/components/alert/alert.component';
import { PanelComponent } from "../../shared/components/panel/panel.component";
import { SelectInputComponent } from 'src/app/shared/components/select-input/select-input.component';
import { NumberInputComponent } from 'src/app/shared/components/number-input/number-input.component';
import { SearchbarModule } from 'src/app/shared/modules/searchbar/searchbar.module';
import { ChipComponent } from 'src/app/shared/components/chip/chip.component';
import { RouterModule } from '@angular/router';
import { routes } from './publications.routes';

@NgModule({
  declarations: [
    PublicationListComponent,
    PublicationAddComponent,
    PublicationUpdateComponent,
    PublicationDetailsComponent,
    PublicationFormComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    PaginationComponent,
    TextInputComponent,
    TextareaInputComponent,
    DateInputComponent,
    AlertComponent,
    PanelComponent,
    SelectInputComponent,
    NumberInputComponent,
    ChipComponent,
    SearchbarModule
  ]
})
export class PublicationsModule { }
