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
