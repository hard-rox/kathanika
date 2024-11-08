/* eslint-disable @typescript-eslint/no-empty-function */
export const mockQueryGql = {
    watch: () => {
        return {
            valueChanges: {
                subscribe: () => {},
            },
            refetch: () => {},
        };
    },
    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    fetch: (variables?: never) => {
        return {
            subscribe: () => {},
            pipe: () => {
                return {
                    subscribe: () => {},
                };
            },
        };
    },
};

export const mockMutationGql = {
    mutate: () => {
        // eslint-disable-next-line no-empty-pattern
        return { subscribe: ({}) => {} };
    },
};
