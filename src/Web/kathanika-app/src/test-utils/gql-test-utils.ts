export const mockQueryGql = {
  watch: () => {
    return {
      valueChanges: {
        subscribe: () => {},
      },
      refetch: () => {},
    };
  },
};

export const mockMutatuionGql = {
  mutate: () => {},
};
