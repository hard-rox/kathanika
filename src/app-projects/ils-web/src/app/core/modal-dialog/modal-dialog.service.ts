import {Injectable, Type} from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class ModalDialogService {
    // private easeModalService = inject(ModalService);

    open(component: Type<unknown>): Promise<{
        closedOnClickOrEscape: boolean
        data: unknown
    }> {
        console.log('open', component);
        return new Promise((resolve) => {
            console.log(resolve);
        });
        // return this.easeModalService.open(component, {
        //     modal: {
        //         enter: 'enter-scale-down 0.1s ease-out'
        //     },
        //     overlay: {
        //         // animation
        //         leave: 'fade-out 0.3s',
        //         backgroundColor: 'rgb(107 114 128 / 0.75)'
        //     },
        //     size: {
        //         // modal configuration
        //         width: '600px'
        //     },
        //     data: {
        //         // data to ModalContentComponent
        //         type: 'Angular modal library',
        //
        //     },
        // });
    }

    close(data: unknown) {
        console.log('close', data);
        // this.easeModalService.close(data);
    }
}
