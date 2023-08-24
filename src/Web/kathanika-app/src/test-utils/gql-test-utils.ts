import { MutationResult } from 'apollo-angular';
import { AddAuthorMutation } from 'src/app/graphql/generated/graphql-operations';

export const mockQueryGql = {
  watch: () => {
    return {
      valueChanges: {
        subscribe: () => {},
      },
      refetch: () => {},
    };
  },
  fetch: (variables?: any) => {
    return { subscribe: () => {} };
  },
};

export const mockMutationGql = {
  mutate: () => {
    return { subscribe: ({}) => {} };
  },
};
