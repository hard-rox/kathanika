import {OperationVariables} from '@apollo/client/core';
import {Query, QueryRef} from 'apollo-angular';

export abstract class BaseQueryComponent<
    TQuery,
    TQueryVariables extends OperationVariables,
> {
    private readonly _queryRef!: QueryRef<TQuery, TQueryVariables>;
    public get queryRef() {
        return this._queryRef;
    }

    private _queryVariables!: TQueryVariables;
    public get queryVariables(): TQueryVariables {
        return this._queryVariables;
    }

    protected set queryVariables(
        queryVariable: TQueryVariables | null | undefined,
    ) {
        if (queryVariable) this._queryVariables = queryVariable;
    }

    protected constructor(
        gql: Query<TQuery, TQueryVariables>,
        queryVariables?: TQueryVariables | null,
    ) {
        this.queryVariables = queryVariables;
        this._queryRef = gql.watch({variables: this._queryVariables} as any);
    }
}