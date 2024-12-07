import {inject, Provider} from "@angular/core";
import {provideApollo} from "apollo-angular";
import {HttpLink} from "apollo-angular/http";
import {InMemoryCache} from "@apollo/client/core";

export function provideGraphqlClient(graphqlEndpoint: string): Provider {
    return provideApollo(() => {
            return {
                link: inject(HttpLink).create({uri: graphqlEndpoint}),
                cache: new InMemoryCache()
            };
        },
        {
            useMutationLoading: true,
        });
}