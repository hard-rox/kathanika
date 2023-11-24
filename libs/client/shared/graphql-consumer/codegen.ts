import type { CodegenConfig } from '@graphql-codegen/cli';

const config: CodegenConfig = {
  schema: 'http://localhost:5289/graphql',
  documents: 'libs/client/shared/graphql-consumer/src/lib/graphql/**/*.graphql',
  generates: {
    'libs/client/shared/graphql-consumer/src/lib/graphql/generated/graphql-operations.ts': {
      plugins: [
        'typescript',
        'typescript-operations',
        'typescript-apollo-angular',
      ],
    },
  },
};
export default config;
