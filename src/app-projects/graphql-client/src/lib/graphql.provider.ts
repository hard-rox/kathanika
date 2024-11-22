import {inject, Provider} from "@angular/core";
import {provideApollo} from "apollo-angular";
import {HttpLink} from "apollo-angular/http";
import {InMemoryCache} from "@apollo/client/core";

export function provideGraphqlClient(graphqlEndpoint: string): Provider {
    return provideApollo(() => {
        const httpLink = inject(HttpLink);
        return {
            link: httpLink.create({uri: graphqlEndpoint}),
            cache: new InMemoryCache()
        }
    });
}