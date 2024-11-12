import {Provider} from "@angular/core";
import {FILE_SERVER} from "../../services/file-uploader/file-uploader.service";

export function provideTusFileServer(tusFileServerEndpoint: string): Provider {
    return {
        provide: FILE_SERVER,
        useValue: tusFileServerEndpoint,
    }
}