import {Component} from '@angular/core';
import {ReactiveFormsModule} from "@angular/forms";
import {KnButton} from "../../directives/button/button.directive";

@Component({
    selector: 'kn-modal',
    imports: [
        ReactiveFormsModule,
        KnButton
    ],
    templateUrl: './modal.component.html'
})
export class KnModal {
    protected opened = false;

    show() {
        console.log("Modal opened");
        this.opened = true;
    }

    close() {
        console.debug('modal closed');
        this.opened = false;
    }
}
