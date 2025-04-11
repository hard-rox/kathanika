import {Injectable} from '@angular/core';
import {from, map, Observable} from "rxjs";
import Swal from 'sweetalert2';
import {ApolloError} from '@apollo/client/errors';

@Injectable({
    providedIn: 'root'
})
export class MessageAlertService {
    private themedSwal = Swal.mixin({
        customClass: {
            popup: 'rounded-none',
            confirmButton:
                'bg-theme-gunmetal hover:bg-theme-rich-black active:bg-theme-rich-black px-4 py-2 text-white mx-2',
            cancelButton:
                'bg-theme-davys-gray hover:bg-theme-gunmetal active:bg-theme-rich-black px-4 py-2 text-white mx-2',
        },
        buttonsStyling: false,
    });

    showPopup(
        type: 'success' | 'warning' | 'info' | 'question' | 'error',
        message: string,
        title?: string,
    ) {
        this.themedSwal.fire({
            icon: type,
            title: title ?? (type === 'success'
                ? 'Success'
                : type === 'warning'
                    ? 'Warning'
                    : type === 'info'
                        ? 'Information'
                        : type === 'question'
                            ? 'Question'
                            : 'Error'),
            text: message,
        });
    }

    showConfirmation(
        confirmationType: 'warning' | 'question',
        message: string,
        title?: string,
        confirmButtonText?: string,
        cancelButtonText?: string,
    ): Observable<boolean> {
        const confirmationSwal = Swal.mixin({
            customClass: {
                popup: 'rounded-none',
                confirmButton:
                    confirmationType == 'warning'
                        ? 'bg-theme-fire-red bg-opacity-80 hover:bg-opacity-100 active:bg-opacity-100 px-4 py-2 text-white mx-2'
                        : 'bg-theme-gunmetal hover:bg-theme-rich-black active:bg-theme-rich-black px-4 py-2 text-white mx-2',
                cancelButton:
                    'bg-theme-davys-gray hover:bg-theme-gunmetal active:bg-theme-rich-black px-4 py-2 text-white mx-2',
            },
            buttonsStyling: false,
        });
        const promise = confirmationSwal.fire({
            icon: confirmationType == 'warning' ? 'warning' : 'question',
            title: title ? title : 'Are you sure?',
            text: message,
            showCancelButton: true,
            reverseButtons: true,
            confirmButtonText: confirmButtonText ? confirmButtonText : 'Confirm',
            cancelButtonText: cancelButtonText ? cancelButtonText : 'Cancel',
            allowOutsideClick: false,
            allowEscapeKey: false,
        });
        return from(promise).pipe(map((swalValue) => swalValue.isConfirmed));
    }

    showToast(
        type: 'success' | 'warning' | 'info' | 'question' | 'error',
        message: string,
    ) {
        const toastSwal = Swal.mixin({
            toast: true,
            position: 'top-end',
            showConfirmButton: false,
            timer: 3000,
            timerProgressBar: true,
            didOpen: (toast) => {
                toast.addEventListener('mouseenter', Swal.stopTimer);
                toast.addEventListener('mouseleave', Swal.resumeTimer);
            },
        });
        toastSwal.fire({
            icon: type,
            title: message,
        });
    }

    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    showHttpErrorPopup(error: ApolloError | any) {
        console.debug(typeof error, typeof ApolloError, JSON.stringify(error))
        if (error instanceof ApolloError) {
            this.showPopup('error',
                error?.networkError?.message ?? 'There is an unfortunate GraphQL Error');
        }
    }
}
