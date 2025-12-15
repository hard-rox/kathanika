import { ApplicationConfig, provideBrowserGlobalErrorListeners, isDevMode, DEFAULT_CURRENCY_CODE } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideClientHydration, withEventReplay, withIncrementalHydration } from '@angular/platform-browser';
import { provideServiceWorker } from '@angular/service-worker';
import { provideHttpClient, withFetch } from '@angular/common/http';
import { provideGraphqlClient } from './graphql/graphql.provider';
import { environment } from '../environments/environment.development';
import { DATE_PIPE_DEFAULT_OPTIONS } from '@angular/common';

export const appConfig: ApplicationConfig = {
    providers: [
        provideBrowserGlobalErrorListeners(),
        provideRouter(routes),
        provideClientHydration(
            withIncrementalHydration(),
            withEventReplay()
        ),
        provideServiceWorker('ngsw-worker.js', {
            enabled: !isDevMode(),
            registrationStrategy: 'registerWhenStable:30000'
        }),
        provideHttpClient(withFetch()),
        provideGraphqlClient(environment.graphqlServer),
        // provideTusFileServer(environment.fileServer),
        {
            provide: DEFAULT_CURRENCY_CODE,
            useValue: 'BDT '
        },
        {
            provide: DATE_PIPE_DEFAULT_OPTIONS,
            useValue: 'dd/MMM/yyyy'
        }
    ]
};
