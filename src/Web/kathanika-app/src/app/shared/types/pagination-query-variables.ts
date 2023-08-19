import { OperationVariables } from "@apollo/client/core";

export interface PaginationQueryVariables extends OperationVariables {
  skip: number,
  take: number
}
