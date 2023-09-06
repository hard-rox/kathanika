import { gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
import * as Apollo from 'apollo-angular';
export type Maybe<T> = T | null;
export type InputMaybe<T> = Maybe<T>;
export type Exact<T extends { [key: string]: unknown }> = { [K in keyof T]: T[K] };
export type MakeOptional<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]?: Maybe<T[SubKey]> };
export type MakeMaybe<T, K extends keyof T> = Omit<T, K> & { [SubKey in K]: Maybe<T[SubKey]> };
export type MakeEmpty<T extends { [key: string]: unknown }, K extends keyof T> = { [_ in K]?: never };
export type Incremental<T> = T | { [P in keyof T]?: P extends ' $fragmentName' | '__typename' ? T[P] : never };
/** All built-in and custom scalars, mapped to their actual values */
export type Scalars = {
  ID: { input: string; output: string; }
  String: { input: string; output: string; }
  Boolean: { input: boolean; output: boolean; }
  Int: { input: number; output: number; }
  Float: { input: number; output: number; }
  /** The `Date` scalar represents an ISO-8601 compliant date type. */
  Date: { input: any; output: any; }
  /** The `DateTime` scalar represents an ISO-8601 compliant date time type. */
  DateTime: { input: any; output: any; }
  /** The built-in `Decimal` scalar type. */
  Decimal: { input: any; output: any; }
};

export type AddAuthorError = InvalidFieldError;

export type AddAuthorInput = {
  biography: Scalars['String']['input'];
  dateOfBirth: Scalars['Date']['input'];
  dateOfDeath?: InputMaybe<Scalars['Date']['input']>;
  firstName: Scalars['String']['input'];
  lastName: Scalars['String']['input'];
  nationality: Scalars['String']['input'];
};

export type AddAuthorPayload = {
  __typename?: 'AddAuthorPayload';
  data?: Maybe<Author>;
  errors?: Maybe<Array<AddAuthorError>>;
  message?: Maybe<Scalars['String']['output']>;
};

export type AddPublicationError = InvalidFieldError;

export type AddPublicationInput = {
  authorIds: Array<Scalars['String']['input']>;
  buyingPrice: Scalars['Decimal']['input'];
  callNumber: Scalars['String']['input'];
  copiesPurchased: Scalars['Int']['input'];
  isbn: Scalars['String']['input'];
  publicationType: PublicationType;
  publishedDate: Scalars['Date']['input'];
  publisher: Scalars['String']['input'];
  title: Scalars['String']['input'];
};

export type AddPublicationPayload = {
  __typename?: 'AddPublicationPayload';
  data?: Maybe<Publication>;
  errors?: Maybe<Array<AddPublicationError>>;
  message?: Maybe<Scalars['String']['output']>;
};

export type Author = {
  __typename?: 'Author';
  biography: Scalars['String']['output'];
  dateOfBirth: Scalars['Date']['output'];
  dateOfDeath?: Maybe<Scalars['Date']['output']>;
  firstName: Scalars['String']['output'];
  fullName: Scalars['String']['output'];
  id: Scalars['String']['output'];
  lastName: Scalars['String']['output'];
  nationality: Scalars['String']['output'];
};

export type AuthorFilterInput = {
  and?: InputMaybe<Array<AuthorFilterInput>>;
  dateOfBirth?: InputMaybe<DateOperationFilterInput>;
  dateOfDeath?: InputMaybe<DateOperationFilterInput>;
  firstName?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  lastName?: InputMaybe<StringOperationFilterInput>;
  nationality?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<AuthorFilterInput>>;
};

export type AuthorPatchInput = {
  biography?: InputMaybe<Scalars['String']['input']>;
  dateOfBirth?: InputMaybe<Scalars['Date']['input']>;
  firstName?: InputMaybe<Scalars['String']['input']>;
  lastName?: InputMaybe<Scalars['String']['input']>;
  nationality?: InputMaybe<Scalars['String']['input']>;
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
  totalCount: Scalars['Int']['output'];
};

/** Information about the offset pagination. */
export type CollectionSegmentInfo = {
  __typename?: 'CollectionSegmentInfo';
  /** Indicates whether more items exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean']['output'];
  /** Indicates whether more items exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean']['output'];
};

export type DateOperationFilterInput = {
  eq?: InputMaybe<Scalars['Date']['input']>;
  gt?: InputMaybe<Scalars['Date']['input']>;
  gte?: InputMaybe<Scalars['Date']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Date']['input']>>>;
  lt?: InputMaybe<Scalars['Date']['input']>;
  lte?: InputMaybe<Scalars['Date']['input']>;
  neq?: InputMaybe<Scalars['Date']['input']>;
  ngt?: InputMaybe<Scalars['Date']['input']>;
  ngte?: InputMaybe<Scalars['Date']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Date']['input']>>>;
  nlt?: InputMaybe<Scalars['Date']['input']>;
  nlte?: InputMaybe<Scalars['Date']['input']>;
};

export type DecimalOperationFilterInput = {
  eq?: InputMaybe<Scalars['Decimal']['input']>;
  gt?: InputMaybe<Scalars['Decimal']['input']>;
  gte?: InputMaybe<Scalars['Decimal']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Decimal']['input']>>>;
  lt?: InputMaybe<Scalars['Decimal']['input']>;
  lte?: InputMaybe<Scalars['Decimal']['input']>;
  neq?: InputMaybe<Scalars['Decimal']['input']>;
  ngt?: InputMaybe<Scalars['Decimal']['input']>;
  ngte?: InputMaybe<Scalars['Decimal']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Decimal']['input']>>>;
  nlt?: InputMaybe<Scalars['Decimal']['input']>;
  nlte?: InputMaybe<Scalars['Decimal']['input']>;
};

export type DeleteAuthorError = DeletionFailedError | NotFoundWithTheIdError;

export type DeleteAuthorInput = {
  id: Scalars['String']['input'];
};

export type DeleteAuthorPayload = {
  __typename?: 'DeleteAuthorPayload';
  errors?: Maybe<Array<DeleteAuthorError>>;
  message?: Maybe<Scalars['String']['output']>;
};

export type DeletionFailedError = Error & {
  __typename?: 'DeletionFailedError';
  message: Scalars['String']['output'];
  objectName: Scalars['String']['output'];
  reason: Scalars['String']['output'];
};

export type Error = {
  message: Scalars['String']['output'];
};

export type FireNewNotificationInput = {
  content: Scalars['String']['input'];
};

export type FireNewNotificationPayload = {
  __typename?: 'FireNewNotificationPayload';
  notification?: Maybe<Notification>;
};

export type IntOperationFilterInput = {
  eq?: InputMaybe<Scalars['Int']['input']>;
  gt?: InputMaybe<Scalars['Int']['input']>;
  gte?: InputMaybe<Scalars['Int']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['Int']['input']>>>;
  lt?: InputMaybe<Scalars['Int']['input']>;
  lte?: InputMaybe<Scalars['Int']['input']>;
  neq?: InputMaybe<Scalars['Int']['input']>;
  ngt?: InputMaybe<Scalars['Int']['input']>;
  ngte?: InputMaybe<Scalars['Int']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['Int']['input']>>>;
  nlt?: InputMaybe<Scalars['Int']['input']>;
  nlte?: InputMaybe<Scalars['Int']['input']>;
};

export type InvalidFieldError = Error & {
  __typename?: 'InvalidFieldError';
  fieldName: Scalars['String']['output'];
  message: Scalars['String']['output'];
};

export type ListFilterInputTypeOfPublicationAuthorFilterInput = {
  all?: InputMaybe<PublicationAuthorFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<PublicationAuthorFilterInput>;
  some?: InputMaybe<PublicationAuthorFilterInput>;
};

export type Mutations = {
  __typename?: 'Mutations';
  addAuthor: AddAuthorPayload;
  addPublication: AddPublicationPayload;
  deleteAuthor: DeleteAuthorPayload;
  /** @deprecated Just a dummy for throwing new notification... */
  fireNewNotification: FireNewNotificationPayload;
  updateAuthor: UpdateAuthorPayload;
  updatePublication: UpdatePublicationPayload;
};


export type MutationsAddAuthorArgs = {
  input: AddAuthorInput;
};


export type MutationsAddPublicationArgs = {
  input: AddPublicationInput;
};


export type MutationsDeleteAuthorArgs = {
  input: DeleteAuthorInput;
};


export type MutationsFireNewNotificationArgs = {
  input: FireNewNotificationInput;
};


export type MutationsUpdateAuthorArgs = {
  input: UpdateAuthorInput;
};


export type MutationsUpdatePublicationArgs = {
  input: UpdatePublicationInput;
};

export type NotFoundWithTheIdError = Error & {
  __typename?: 'NotFoundWithTheIdError';
  id: Scalars['String']['output'];
  message: Scalars['String']['output'];
  objectName: Scalars['String']['output'];
};

export type Notification = {
  __typename?: 'Notification';
  message?: Maybe<Scalars['String']['output']>;
};

export type Publication = {
  __typename?: 'Publication';
  authors: Array<PublicationAuthor>;
  buyingPrice: Scalars['Decimal']['output'];
  callNumber: Scalars['String']['output'];
  copiesAvailable: Scalars['Int']['output'];
  description: Scalars['String']['output'];
  edition: Scalars['String']['output'];
  id: Scalars['String']['output'];
  isbn?: Maybe<Scalars['String']['output']>;
  language: Scalars['String']['output'];
  publicationType: PublicationType;
  publishedDate: Scalars['Date']['output'];
  publisher: Scalars['String']['output'];
  title: Scalars['String']['output'];
};

export type PublicationAuthor = {
  __typename?: 'PublicationAuthor';
  firstName: Scalars['String']['output'];
  id: Scalars['String']['output'];
  lastName: Scalars['String']['output'];
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
  publishedDate?: InputMaybe<DateOperationFilterInput>;
  publisher?: InputMaybe<StringOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
};

export type PublicationPatchInput = {
  authorIds?: InputMaybe<Array<Scalars['String']['input']>>;
  buyingPrice?: InputMaybe<Scalars['Decimal']['input']>;
  callNumber: Scalars['String']['input'];
  copiesAvailable?: InputMaybe<Scalars['Int']['input']>;
  isbn: Scalars['String']['input'];
  publicationType: PublicationType;
  publishedDate?: InputMaybe<Scalars['Date']['input']>;
  publisher: Scalars['String']['input'];
  title: Scalars['String']['input'];
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

/** A segment of a collection. */
export type PublicationsCollectionSegment = {
  __typename?: 'PublicationsCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Publication>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

export type Queries = {
  __typename?: 'Queries';
  author?: Maybe<Author>;
  authors?: Maybe<AuthorsCollectionSegment>;
  publication?: Maybe<Publication>;
  publications?: Maybe<PublicationsCollectionSegment>;
};


export type QueriesAuthorArgs = {
  id: Scalars['String']['input'];
};


export type QueriesAuthorsArgs = {
  order?: InputMaybe<Array<AuthorSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<AuthorFilterInput>;
};


export type QueriesPublicationArgs = {
  id: Scalars['String']['input'];
};


export type QueriesPublicationsArgs = {
  order?: InputMaybe<Array<PublicationSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<PublicationFilterInput>;
};

export enum SortEnumType {
  Asc = 'ASC',
  Desc = 'DESC'
}

export type StringOperationFilterInput = {
  and?: InputMaybe<Array<StringOperationFilterInput>>;
  contains?: InputMaybe<Scalars['String']['input']>;
  endsWith?: InputMaybe<Scalars['String']['input']>;
  eq?: InputMaybe<Scalars['String']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  ncontains?: InputMaybe<Scalars['String']['input']>;
  nendsWith?: InputMaybe<Scalars['String']['input']>;
  neq?: InputMaybe<Scalars['String']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['String']['input']>>>;
  nstartsWith?: InputMaybe<Scalars['String']['input']>;
  or?: InputMaybe<Array<StringOperationFilterInput>>;
  startsWith?: InputMaybe<Scalars['String']['input']>;
};

export type Subscriptions = {
  __typename?: 'Subscriptions';
  onNewNotification: Notification;
};

export type UpdateAuthorError = InvalidFieldError | NotFoundWithTheIdError;

export type UpdateAuthorInput = {
  id: Scalars['String']['input'];
  patch: AuthorPatchInput;
};

export type UpdateAuthorPayload = {
  __typename?: 'UpdateAuthorPayload';
  data?: Maybe<Author>;
  errors?: Maybe<Array<UpdateAuthorError>>;
  message?: Maybe<Scalars['String']['output']>;
};

export type UpdatePublicationError = InvalidFieldError | NotFoundWithTheIdError;

export type UpdatePublicationInput = {
  id: Scalars['String']['input'];
  patch: PublicationPatchInput;
};

export type UpdatePublicationPayload = {
  __typename?: 'UpdatePublicationPayload';
  data?: Maybe<Publication>;
  errors?: Maybe<Array<UpdatePublicationError>>;
  message?: Maybe<Scalars['String']['output']>;
};

export type AddAuthorMutationVariables = Exact<{
  addAuthorInput: AddAuthorInput;
}>;


export type AddAuthorMutation = { __typename?: 'Mutations', addAuthor: { __typename?: 'AddAuthorPayload', message?: string | null, data?: { __typename?: 'Author', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string }> | null } };

export type UpdateAuthorMutationVariables = Exact<{
  id: Scalars['String']['input'];
  authorPatch: AuthorPatchInput;
}>;


export type UpdateAuthorMutation = { __typename?: 'Mutations', updateAuthor: { __typename?: 'UpdateAuthorPayload', message?: string | null, data?: { __typename?: 'Author', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string } | { __typename?: 'NotFoundWithTheIdError', id: string, objectName: string, message: string }> | null } };

export type AddPublicationMutationVariables = Exact<{
  addPublicationInput: AddPublicationInput;
}>;


export type AddPublicationMutation = { __typename?: 'Mutations', addPublication: { __typename?: 'AddPublicationPayload', message?: string | null, data?: { __typename?: 'Publication', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string }> | null } };

export type UpdatePublicationMutationVariables = Exact<{
  id: Scalars['String']['input'];
  publicationPatch: PublicationPatchInput;
}>;


export type UpdatePublicationMutation = { __typename?: 'Mutations', updatePublication: { __typename?: 'UpdatePublicationPayload', message?: string | null, data?: { __typename?: 'Publication', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string } | { __typename?: 'NotFoundWithTheIdError', id: string, objectName: string, message: string }> | null } };

export type GetAuthorsQueryVariables = Exact<{
  skip: Scalars['Int']['input'];
  take: Scalars['Int']['input'];
  filter?: InputMaybe<AuthorFilterInput>;
  sortBy?: InputMaybe<Array<AuthorSortInput> | AuthorSortInput>;
}>;


export type GetAuthorsQuery = { __typename?: 'Queries', authors?: { __typename?: 'AuthorsCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Author', id: string, firstName: string, lastName: string, nationality: string }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type GetAuthorQueryVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type GetAuthorQuery = { __typename?: 'Queries', author?: { __typename?: 'Author', id: string, firstName: string, lastName: string, fullName: string, dateOfBirth: any, dateOfDeath?: any | null, nationality: string, biography: string } | null };

export type GetPublicationsQueryVariables = Exact<{
  skip: Scalars['Int']['input'];
  take: Scalars['Int']['input'];
  filter?: InputMaybe<PublicationFilterInput>;
  sortBy?: InputMaybe<Array<PublicationSortInput> | PublicationSortInput>;
}>;


export type GetPublicationsQuery = { __typename?: 'Queries', publications?: { __typename?: 'PublicationsCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Publication', id: string, title: string, callNumber: string, publicationType: PublicationType, publisher: string, language: string, copiesAvailable: number, authors: Array<{ __typename?: 'PublicationAuthor', firstName: string, lastName: string }> }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type GetPublicationQueryVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type GetPublicationQuery = { __typename?: 'Queries', publication?: { __typename?: 'Publication', id: string, title: string, publicationType: PublicationType, isbn?: string | null, edition: string, callNumber: string, language: string, publisher: string, publishedDate: any, buyingPrice: any, copiesAvailable: number, description: string, authors: Array<{ __typename?: 'PublicationAuthor', id: string, firstName: string, lastName: string }> } | null };

export const AddAuthorDocument = gql`
    mutation addAuthor($addAuthorInput: AddAuthorInput!) {
  addAuthor(input: $addAuthorInput) {
    message
    data {
      id
    }
    errors {
      ... on InvalidFieldError {
        fieldName
      }
      ... on Error {
        message
      }
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class AddAuthorGQL extends Apollo.Mutation<AddAuthorMutation, AddAuthorMutationVariables> {
    document = AddAuthorDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const UpdateAuthorDocument = gql`
    mutation updateAuthor($id: String!, $authorPatch: AuthorPatchInput!) {
  updateAuthor(input: {id: $id, patch: $authorPatch}) {
    message
    data {
      id
    }
    errors {
      ... on NotFoundWithTheIdError {
        id
        objectName
      }
      ... on InvalidFieldError {
        fieldName
      }
      ... on Error {
        message
      }
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class UpdateAuthorGQL extends Apollo.Mutation<UpdateAuthorMutation, UpdateAuthorMutationVariables> {
    document = UpdateAuthorDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const AddPublicationDocument = gql`
    mutation addPublication($addPublicationInput: AddPublicationInput!) {
  addPublication(input: $addPublicationInput) {
    message
    data {
      id
    }
    errors {
      ... on InvalidFieldError {
        fieldName
      }
      ... on Error {
        message
      }
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class AddPublicationGQL extends Apollo.Mutation<AddPublicationMutation, AddPublicationMutationVariables> {
    document = AddPublicationDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const UpdatePublicationDocument = gql`
    mutation updatePublication($id: String!, $publicationPatch: PublicationPatchInput!) {
  updatePublication(input: {id: $id, patch: $publicationPatch}) {
    message
    data {
      id
    }
    errors {
      ... on NotFoundWithTheIdError {
        id
        objectName
      }
      ... on InvalidFieldError {
        fieldName
      }
      ... on Error {
        message
      }
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class UpdatePublicationGQL extends Apollo.Mutation<UpdatePublicationMutation, UpdatePublicationMutationVariables> {
    document = UpdatePublicationDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
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
    fullName
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
export const GetPublicationsDocument = gql`
    query getPublications($skip: Int!, $take: Int!, $filter: PublicationFilterInput, $sortBy: [PublicationSortInput!]) {
  publications(skip: $skip, take: $take, where: $filter, order: $sortBy) {
    items {
      id
      title
      authors {
        firstName
        lastName
      }
      callNumber
      publicationType
      publisher
      language
      copiesAvailable
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
  export class GetPublicationsGQL extends Apollo.Query<GetPublicationsQuery, GetPublicationsQueryVariables> {
    document = GetPublicationsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const GetPublicationDocument = gql`
    query getPublication($id: String!) {
  publication(id: $id) {
    id
    title
    publicationType
    isbn
    edition
    callNumber
    authors {
      id
      firstName
      lastName
    }
    language
    publisher
    publishedDate
    buyingPrice
    copiesAvailable
    description
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class GetPublicationGQL extends Apollo.Query<GetPublicationQuery, GetPublicationQueryVariables> {
    document = GetPublicationDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }