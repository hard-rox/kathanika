import { ModuleWithProviders, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FILE_SERVER } from '../../services/file-uploader/file-uploader.service';

@NgModule({
    declarations: [],
    imports: [CommonModule],
})
export class FileServerModule {
    static forRoot(fileServer: string): ModuleWithProviders<FileServerModule> {
        return {
            ngModule: FileServerModule,
            providers: [
                {
                    provide: FILE_SERVER,
                    useValue: fileServer,
                },
            ],
        };
    }
}
