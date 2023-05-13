import { OperationVariables } from "@apollo/client/core";
import { Apollo, Query, QueryRef } from "apollo-angular";

export class BaseQueryComponent<TQuery, TQueryVariable extends OperationVariables>{
    private _queryRef!: QueryRef<TQuery, TQueryVariable>;
    public get queryRef() {
        return this._queryRef;
    }

    private _queryVariable!: TQueryVariable;
    public get queryVariable() {
        return this._queryVariable;
    }
    protected set queryVariable(queryVariable: TQueryVariable | null) {
        if(queryVariable) this._queryVariable = queryVariable;
    }

    constructor(
        gql: Query<TQuery, TQueryVariable>,
        queryVariable: TQueryVariable | null) {
        this._queryVariable = queryVariable as TQueryVariable
        this._queryRef = gql.watch(this._queryVariable);
    }
}