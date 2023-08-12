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

export const mockMutatuionGql = {
  mutate: () => {
    return { subscribe: ({}) => {} };
  },
};
