import { gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
import * as Apollo from 'apollo-angular';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: string;
  String: string;
  Boolean: boolean;
  Int: number;
  Float: number;
  /** The `Date` scalar represents an ISO-8601 compliant date type. */
  Date: any;
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: any;
  /** The built-in `Decimal` scalar type. */
  Decimal: any;
};

export type AddAuthorError = InvalidFieldError;

export type AddAuthorInput = {
  biography: Scalars['String'];
  dateOfBirth: Scalars['DateTime'];
  dateOfDeath?: InputMaybe<Scalars['DateTime']>;
  firstName: Scalars['String'];
  lastName: Scalars['String'];
  nationality: Scalars['String'];
};

export type AddAuthorPayload = {
  __typename?: 'AddAuthorPayload';
  data?: Maybe<Author>;
  errors?: Maybe<Array<AddAuthorError>>;
  message?: Maybe<Scalars['String']>;
};

export type AddPublicationCommandInput = {
  authorIds: Array<Scalars['String']>;
  buyingPrice: Scalars['Decimal'];
  callNumber: Scalars['String'];
  copiesPurchased: Scalars['Int'];
  isbn: Scalars['String'];
  publicationType: PublicationType;
  publishedDate: Scalars['DateTime'];
  publisher: Scalars['String'];
  title: Scalars['String'];
};

export type AddPublicationError = InvalidFieldError;

export type AddPublicationInput = {
  __typename?: 'AddPublicationInput';
  authorIds: Array<Scalars['String']>;
  buyingPrice: Scalars['Decimal'];
  callNumber: Scalars['String'];
  copiesPurchased: Scalars['Int'];
  isbn: Scalars['String'];
  publicationType: PublicationType;
  publishedDate: Scalars['DateTime'];
  publisher: Scalars['String'];
  title: Scalars['String'];
};

export type AddPublicationPayload = {
  __typename?: 'AddPublicationPayload';
  data?: Maybe<Publication>;
  errors?: Maybe<Array<AddPublicationError>>;
  message?: Maybe<Scalars['String']>;
};

export type Author = {
  __typename?: 'Author';
  biography: Scalars['String'];
  dateOfBirth: Scalars['Date'];
  dateOfDeath?: Maybe<Scalars['Date']>;
  firstName: Scalars['String'];
  fullName: Scalars['String'];
  id: Scalars['String'];
  lastName: Scalars['String'];
  nationality: Scalars['String'];
};

export type AuthorFilterInput = {
  and?: InputMaybe<Array<AuthorFilterInput>>;
  biography?: InputMaybe<StringOperationFilterInput>;
  dateOfBirth?: InputMaybe<DateTimeOperationFilterInput>;
  dateOfDeath?: InputMaybe<DateTimeOperationFilterInput>;
  firstName?: InputMaybe<StringOperationFilterInput>;
  fullName?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  lastName?: InputMaybe<StringOperationFilterInput>;
  nationality?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<AuthorFilterInput>>;
};

export type AuthorPatchInput = {
  biography?: InputMaybe<Scalars['String']>;
  dateOfBirth?: InputMaybe<Scalars['DateTime']>;
  firstName?: InputMaybe<Scalars['String']>;
  lastName?: InputMaybe<Scalars['String']>;
  nationality?: InputMaybe<Scalars['String']>;
};

export type AuthorSortInput = {
  biography?: InputMaybe<SortEnumType>;
  dateOfBirth?: InputMaybe<SortEnumType>;
  dateOfDeath?: InputMaybe<SortEnumType>;
  firstName?: InputMaybe<SortEnumType>;
  fullName?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  lastName?: InputMaybe<SortEnumType>;
  nationality?: InputMaybe<SortEnumType>;
};

/** A segment of a collection. */
export type AuthorsCollectionSegment = {
  __typename?: 'AuthorsCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Author>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int'];
};

/** Information about the offset pagination. */
export type CollectionSegmentInfo = {
  __typename?: 'CollectionSegmentInfo';
  /** Indicates whether more items exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean'];
  /** Indicates whether more items exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean'];
};

export type DateTimeOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']>;
  gt?: InputMaybe<Scalars['DateTime']>;
  gte?: InputMaybe<Scalars['DateTime']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['DateTime']>>>;
  lt?: InputMaybe<Scalars['DateTime']>;
  lte?: InputMaybe<Scalars['DateTime']>;
  neq?: InputMaybe<Scalars['DateTime']>;
  ngt?: InputMaybe<Scalars['DateTime']>;
  ngte?: InputMaybe<Scalars['DateTime']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['DateTime']>>>;
  nlt?: InputMaybe<Scalars['DateTime']>;
  nlte?: InputMaybe<Scalars['DateTime']>;
};

export type DecimalOperationFilterInput = {
  eq?: InputMaybe<Scalars['Decimal']>;
  gt?: InputMaybe<Scalars['Decimal']>;
  gte?: InputMaybe<Scalars['Decimal']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Decimal']>>>;
  lt?: InputMaybe<Scalars['Decimal']>;
  lte?: InputMaybe<Scalars['Decimal']>;
  neq?: InputMaybe<Scalars['Decimal']>;
  ngt?: InputMaybe<Scalars['Decimal']>;
  ngte?: InputMaybe<Scalars['Decimal']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Decimal']>>>;
  nlt?: InputMaybe<Scalars['Decimal']>;
  nlte?: InputMaybe<Scalars['Decimal']>;
};

export type DeleteAuthorError = DeletionFailedError | NotFoundWithTheIdError;

export type DeleteAuthorInput = {
  id: Scalars['String'];
};

export type DeleteAuthorPayload = {
  __typename?: 'DeleteAuthorPayload';
  errors?: Maybe<Array<DeleteAuthorError>>;
  message?: Maybe<Scalars['String']>;
};

export type DeletionFailedError = Error & {
  __typename?: 'DeletionFailedError';
  message: Scalars['String'];
  objectName: Scalars['String'];
  reason: Scalars['String'];
};

export type Error = {
  message: Scalars['String'];
};

export type IntOperationFilterInput = {
  eq?: InputMaybe<Scalars['Int']>;
  gt?: InputMaybe<Scalars['Int']>;
  gte?: InputMaybe<Scalars['Int']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Int']>>>;
  lt?: InputMaybe<Scalars['Int']>;
  lte?: InputMaybe<Scalars['Int']>;
  neq?: InputMaybe<Scalars['Int']>;
  ngt?: InputMaybe<Scalars['Int']>;
  ngte?: InputMaybe<Scalars['Int']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Int']>>>;
  nlt?: InputMaybe<Scalars['Int']>;
  nlte?: InputMaybe<Scalars['Int']>;
};

export type InvalidFieldError = Error & {
  __typename?: 'InvalidFieldError';
  fieldName: Scalars['String'];
  message: Scalars['String'];
};

export type ListFilterInputTypeOfPublicationAuthorFilterInput = {
  all?: InputMaybe<PublicationAuthorFilterInput>;
  any?: InputMaybe<Scalars['Boolean']>;
  none?: InputMaybe<PublicationAuthorFilterInput>;
  some?: InputMaybe<PublicationAuthorFilterInput>;
};

export type Mutations = {
  __typename?: 'Mutations';
  addAuthor: AddAuthorPayload;
  addPublication: AddPublicationPayload;
  deleteAuthor: DeleteAuthorPayload;
  updateAuthor: UpdateAuthorPayload;
};


export type MutationsAddAuthorArgs = {
  input: AddAuthorInput;
};


export type MutationsAddPublicationArgs = {
  input: AddPublicationCommandInput;
};


export type MutationsDeleteAuthorArgs = {
  input: DeleteAuthorInput;
};


export type MutationsUpdateAuthorArgs = {
  input: UpdateAuthorInput;
};

export type NotFoundWithTheIdError = Error & {
  __typename?: 'NotFoundWithTheIdError';
  id: Scalars['String'];
  message: Scalars['String'];
  objectName: Scalars['String'];
};

/** Information about pagination in a connection. */
export type PageInfo = {
  __typename?: 'PageInfo';
  /** When paginating forwards, the cursor to continue. */
  endCursor?: Maybe<Scalars['String']>;
  /** Indicates whether more edges exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean'];
  /** Indicates whether more edges exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean'];
  /** When paginating backwards, the cursor to continue. */
  startCursor?: Maybe<Scalars['String']>;
};

export type Publication = {
  __typename?: 'Publication';
  authors: Array<PublicationAuthor>;
  buyingPrice: Scalars['Decimal'];
  callNumber: Scalars['String'];
  copiesAvailable: Scalars['Int'];
  description: Scalars['String'];
  edition: Scalars['String'];
  id: Scalars['String'];
  isbn?: Maybe<Scalars['String']>;
  language: Scalars['String'];
  publicationType: PublicationType;
  publishedDate: Scalars['DateTime'];
  publisher: Scalars['String'];
  title: Scalars['String'];
};

export type PublicationAuthor = {
  __typename?: 'PublicationAuthor';
  firstName: Scalars['String'];
  id: Scalars['String'];
  lastName: Scalars['String'];
};

export type PublicationAuthorFilterInput = {
  and?: InputMaybe<Array<PublicationAuthorFilterInput>>;
  firstName?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  lastName?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<PublicationAuthorFilterInput>>;
};

export type PublicationFilterInput = {
  and?: InputMaybe<Array<PublicationFilterInput>>;
  authors?: InputMaybe<ListFilterInputTypeOfPublicationAuthorFilterInput>;
  buyingPrice?: InputMaybe<DecimalOperationFilterInput>;
  callNumber?: InputMaybe<StringOperationFilterInput>;
  copiesAvailable?: InputMaybe<IntOperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  edition?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  isbn?: InputMaybe<StringOperationFilterInput>;
  language?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<PublicationFilterInput>>;
  publicationType?: InputMaybe<PublicationTypeOperationFilterInput>;
  publishedDate?: InputMaybe<DateTimeOperationFilterInput>;
  publisher?: InputMaybe<StringOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
};

export type PublicationSortInput = {
  buyingPrice?: InputMaybe<SortEnumType>;
  callNumber?: InputMaybe<SortEnumType>;
  copiesAvailable?: InputMaybe<SortEnumType>;
  description?: InputMaybe<SortEnumType>;
  edition?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  isbn?: InputMaybe<SortEnumType>;
  language?: InputMaybe<SortEnumType>;
  publicationType?: InputMaybe<SortEnumType>;
  publishedDate?: InputMaybe<SortEnumType>;
  publisher?: InputMaybe<SortEnumType>;
  title?: InputMaybe<SortEnumType>;
};

export enum PublicationType {
  Book = 'BOOK',
  Journal = 'JOURNAL'
}

export type PublicationTypeOperationFilterInput = {
  eq?: InputMaybe<PublicationType>;
  in?: InputMaybe<Array<PublicationType>>;
  neq?: InputMaybe<PublicationType>;
  nin?: InputMaybe<Array<PublicationType>>;
};

/** A connection to a list of items. */
export type PublicationsConnection = {
  __typename?: 'PublicationsConnection';
  /** A list of edges. */
  edges?: Maybe<Array<PublicationsEdge>>;
  /** A flattened list of the nodes. */
  nodes?: Maybe<Array<Publication>>;
  /** Information to aid in pagination. */
  pageInfo: PageInfo;
  /** Identifies the total count of items in the connection. */
  totalCount: Scalars['Int'];
};

/** An edge in a connection. */
export type PublicationsEdge = {
  __typename?: 'PublicationsEdge';
  /** A cursor for use in pagination. */
  cursor: Scalars['String'];
  /** The item at the end of the edge. */
  node: Publication;
};

export type Queries = {
  __typename?: 'Queries';
  author: Author;
  authors?: Maybe<AuthorsCollectionSegment>;
  publication: Publication;
  publications?: Maybe<PublicationsConnection>;
};


export type QueriesAuthorArgs = {
  id: Scalars['String'];
};


export type QueriesAuthorsArgs = {
  order?: InputMaybe<Array<AuthorSortInput>>;
  skip?: InputMaybe<Scalars['Int']>;
  take?: InputMaybe<Scalars['Int']>;
  where?: InputMaybe<AuthorFilterInput>;
};


export type QueriesPublicationArgs = {
  id: Scalars['String'];
};


export type QueriesPublicationsArgs = {
  after?: InputMaybe<Scalars['String']>;
  before?: InputMaybe<Scalars['String']>;
  first?: InputMaybe<Scalars['Int']>;
  last?: InputMaybe<Scalars['Int']>;
  order?: InputMaybe<Array<PublicationSortInput>>;
  where?: InputMaybe<PublicationFilterInput>;
};

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC'
}

export type StringOperationFilterInput = {
  and?: InputMaybe<Array<StringOperationFilterInput>>;
  contains?: InputMaybe<Scalars['String']>;
  endsWith?: InputMaybe<Scalars['String']>;
  eq?: InputMaybe<Scalars['String']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['String']>>>;
  ncontains?: InputMaybe<Scalars['String']>;
  nendsWith?: InputMaybe<Scalars['String']>;
  neq?: InputMaybe<Scalars['String']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['String']>>>;
  nstartsWith?: InputMaybe<Scalars['String']>;
  or?: InputMaybe<Array<StringOperationFilterInput>>;
  startsWith?: InputMaybe<Scalars['String']>;
};

export type UpdateAuthorError = InvalidFieldError | NotFoundWithTheIdError;

export type UpdateAuthorInput = {
  id: Scalars['String'];
  patch: AuthorPatchInput;
};

export type UpdateAuthorPayload = {
  __typename?: 'UpdateAuthorPayload';
  data?: Maybe<Author>;
  errors?: Maybe<Array<UpdateAuthorError>>;
  message?: Maybe<Scalars['String']>;
};

export type GetAuthorsQueryVariables = Exact<{
  skip: Scalars['Int'];
  take: Scalars['Int'];
  filter?: InputMaybe<AuthorFilterInput>;
  sortBy?: InputMaybe<Array<AuthorSortInput> | AuthorSortInput>;
}>;


export type GetAuthorsQuery = { __typename?: 'Queries', authors?: { __typename?: 'AuthorsCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Author', id: string, firstName: string, lastName: string, nationality: string }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type GetAuthorQueryVariables = Exact<{
  id: Scalars['String'];
}>;


export type GetAuthorQuery = { __typename?: 'Queries', author: { __typename?: 'Author', id: string, firstName: string, lastName: string, dateOfBirth: any, dateOfDeath?: any | null, nationality: string, biography: string } };

export const GetAuthorsDocument = gql`
    query getAuthors($skip: Int!, $take: Int!, $filter: AuthorFilterInput, $sortBy: [AuthorSortInput!]) {
  authors(skip: $skip, take: $take, where: $filter, order: $sortBy) {
    items {
      id
      firstName
      lastName
      nationality
    }
    pageInfo {
      hasNextPage
      hasPreviousPage
    }
    totalCount
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class GetAuthorsGQL extends Apollo.Query<GetAuthorsQuery, GetAuthorsQueryVariables> {
    document = GetAuthorsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const GetAuthorDocument = gql`
    query getAuthor($id: String!) {
  author(id: $id) {
    id
    firstName
    lastName
    dateOfBirth
    dateOfDeath
    nationality
    biography
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class GetAuthorGQL extends Apollo.Query<GetAuthorQuery, GetAuthorQueryVariables> {
    document = GetAuthorDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }