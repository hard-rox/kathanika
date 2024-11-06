import { ModuleWithProviders, NgModule } from '@angular/core';
import { ApolloModule, APOLLO_OPTIONS } from 'apollo-angular';
import { ApolloClientOptions, InMemoryCache } from '@apollo/client/core';
import { HttpLink } from 'apollo-angular/http';

@NgModule({
  exports: [ApolloModule],
})
export class GraphQLModule {
  public static forRoot(
    graphqlEndpoint: string,
  ): ModuleWithProviders<GraphQLModule> {
    return {
      ngModule: GraphQLModule,
      providers: [
        {
          provide: APOLLO_OPTIONS,
          useFactory: (httpLink: HttpLink): ApolloClientOptions<unknown> => {
            return {
              link: httpLink.create({ uri: graphqlEndpoint }),
              cache: new InMemoryCache(),
            };
          },
          deps: [HttpLink],
        },
      ],
    };
  }
}
