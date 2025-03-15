import {ApplicationConfig, DEFAULT_CURRENCY_CODE, isDevMode, provideZoneChangeDetection} from '@angular/core';
import {provideRouter} from '@angular/router';

import {routes} from './app.routes';
import {provideClientHydration, withIncrementalHydration} from '@angular/platform-browser';
import {provideServiceWorker} from '@angular/service-worker';
import {provideHttpClient, withFetch} from "@angular/common/http";
import {environment} from "../environments/environment";
import {provideTusFileServer} from "@kathanika/kn-ui";
import {provideGraphqlClient} from "./graphql/graphql.provider";
import {DATE_PIPE_DEFAULT_OPTIONS} from "@angular/common";

export const appConfig: ApplicationConfig = {
    providers: [
        provideZoneChangeDetection({eventCoalescing: true}),
        provideRouter(routes),
        provideClientHydration(),
        provideServiceWorker('ngsw-worker.js', {
            enabled: !isDevMode(),
            registrationStrategy: 'registerWhenStable:30000'
        }),
        provideHttpClient(withFetch()),
        provideGraphqlClient(environment.graphqlServer),
        provideTusFileServer(environment.fileServer),
        provideClientHydration(withIncrementalHydration()),
        {
            provide: DEFAULT_CURRENCY_CODE,
            useValue: 'BDT'
        },
        {
            provide: DATE_PIPE_DEFAULT_OPTIONS,
            useValue: 'dd/MMM/yyyy'
        }
    ]
};
