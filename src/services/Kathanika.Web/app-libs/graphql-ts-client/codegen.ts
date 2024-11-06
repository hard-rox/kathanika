import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  schema: 'http://localhost:5289/graphql',
  documents: 'src/services/Kathanika.Web/app-libs/graphql-ts-client/src/lib/graphql/**/*.graphql',
  generates: {
    'src/services/Kathanika.Web/app-libs/graphql-ts-client/src/lib/graphql/generated/graphql-operations.ts':
      {
        plugins: [
          'typescript',
          'typescript-operations',
          'typescript-apollo-angular',
        ],
        config: {
          addExplicitOverride: true,
        },
      },
  },
};
export default config;
