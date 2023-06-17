export const mockQueryGql = {
  watch: () => {
    return {
      valueChanges: {
        subscribe: () => {},
      },
      refetch: () => {},
    };
  },
  fetch: () => {
    return { subscribe: () => {} };
  },
};

export const mockMutatuionGql = {
  mutate: () => {},
};
