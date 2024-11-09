import {ApplicationConfig, importProvidersFrom, isDevMode, provideZoneChangeDetection,} from '@angular/core';
import {provideRouter} from '@angular/router';
import {appRoutes} from './app.routes';
import {provideClientHydration} from '@angular/platform-browser';
import {provideServiceWorker} from '@angular/service-worker';
import {provideHttpClient, withFetch} from "@angular/common/http";
import {GraphQLModule} from "@kathanika/graphql-ts-client";
import {environment} from "../environments/environment";
import {FileServerModule} from "@kathanika/kn-ui";

export const appConfig: ApplicationConfig = {
    providers: [
        provideClientHydration(),
        provideZoneChangeDetection({eventCoalescing: true}),
        provideRouter(appRoutes),
        provideServiceWorker('ngsw-worker.js', {
            enabled: !isDevMode(),
            registrationStrategy: 'registerWhenStable:30000',
        }),
        provideHttpClient(withFetch()),
        importProvidersFrom(
            GraphQLModule.forRoot(environment.graphqlServer),
            FileServerModule.forRoot(environment.fileServer)
        ),
    ],
};
