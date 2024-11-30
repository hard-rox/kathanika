import { Injectable, InjectionToken, inject } from '@angular/core';

export const FILE_SERVER = new InjectionToken<string>('fileServer');

@Injectable({
    providedIn: 'root',
})
export class FileUploaderService {
    fileServer = inject(FILE_SERVER);
}
