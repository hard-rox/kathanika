import { Injectable } from '@angular/core';
import { Observable, from, map } from 'rxjs';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root',
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

  showSuccess(message: string, title?: string) {
    this.themedSwal.fire({
      icon: 'success',
      title: title ? title : 'Success',
      text: message,
    });
  }

  showWarning(message: string, title?: string) {
    this.themedSwal.fire({
      icon: 'warning',
      title: title ? title : 'Warning',
      text: message,
    });
  }

  showError(message: string, title?: string) {
    this.themedSwal.fire({
      icon: 'error',
      title: title ? title : 'Error',
      text: message,
    });
  }

  showConfirmation(
    message: string,
    title?: string,
    confirmButtonText?: string,
    cancelButtonText?: string
  ): Observable<boolean> {
    const promise = this.themedSwal.fire({
      icon: 'question',
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
}
