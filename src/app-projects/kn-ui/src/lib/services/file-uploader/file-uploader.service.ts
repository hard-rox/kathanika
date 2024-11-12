import { Inject, Injectable, InjectionToken } from '@angular/core';

export const FILE_SERVER = new InjectionToken<string>('fileServer');

@Injectable({
  providedIn: 'root',
})
export class FileUploaderService {
  constructor(@Inject(FILE_SERVER) public fileServer: string) {}
}
