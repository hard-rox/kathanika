import type {CodegenConfig} from '@graphql-codegen/cli';

const config: CodegenConfig = {
    schema: 'http://localhost:5289/graphql',
    documents: 'src/app-projects/graphql-client/src/lib/graphql/**/*.graphql',
    generates: {
        'src/app-projects/graphql-client/src/lib/graphql/generated/graphql-operations.ts':
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
