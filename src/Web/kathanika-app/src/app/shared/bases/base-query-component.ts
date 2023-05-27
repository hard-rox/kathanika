import { OperationVariables } from "@apollo/client/core";
import { Query, QueryRef } from "apollo-angular";

export abstract class BaseQueryComponent<TQuery, TQueryVariable extends OperationVariables>{
    private _queryRef!: QueryRef<TQuery, TQueryVariable>;
    public get queryRef() {
        return this._queryRef;
    }

    private _queryVariables!: TQueryVariable;
    public get queryVariables(): TQueryVariable {
        return this._queryVariables;
    }
    protected set queryVariables(queryVariable: TQueryVariable | null | undefined) {
        if(queryVariable) this._queryVariables = queryVariable;
    }

    constructor(
        gql: Query<TQuery, TQueryVariable>,
        queryVariables?: TQueryVariable | null) {
        this.queryVariables = queryVariables;
        this._queryRef = gql.watch(this._queryVariables);
    }
}