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
};

export type AddVendorInput = {
  accountDetail?: InputMaybe<Scalars['String']['input']>;
  address: Scalars['String']['input'];
  contactNumber: Scalars['String']['input'];
  contactPersonEmail?: InputMaybe<Scalars['String']['input']>;
  contactPersonName?: InputMaybe<Scalars['String']['input']>;
  contactPersonPhone?: InputMaybe<Scalars['String']['input']>;
  email?: InputMaybe<Scalars['String']['input']>;
  name: Scalars['String']['input'];
  status: VendorStatus;
  website?: InputMaybe<Scalars['String']['input']>;
};

export type AddVendorPayload = {
  __typename?: 'AddVendorPayload';
  data?: Maybe<Vendor>;
  errors?: Maybe<Array<ErrorType>>;
  message?: Maybe<Scalars['String']['output']>;
};

export type BibRecord = {
  __typename?: 'BibRecord';
  catalogingSource: CatalogingSource;
  controlNumber: Scalars['String']['output'];
  controlNumberIdentifier: Scalars['String']['output'];
  dateAndTimeOfLatestTransaction: Scalars['DateTime']['output'];
  fixedLengthDataElements: Scalars['String']['output'];
  id: Scalars['String']['output'];
  leader: Scalars['String']['output'];
};

export type BibRecordFilterInput = {
  and?: InputMaybe<Array<BibRecordFilterInput>>;
  catalogingSource?: InputMaybe<CatalogingSourceFilterInput>;
  controlNumber?: InputMaybe<StringOperationFilterInput>;
  controlNumberIdentifier?: InputMaybe<StringOperationFilterInput>;
  createdAt?: InputMaybe<DateTimeOperationFilterInput>;
  createdByUserId?: InputMaybe<StringOperationFilterInput>;
  createdByUserName?: InputMaybe<StringOperationFilterInput>;
  dateAndTimeOfLatestTransaction?: InputMaybe<DateTimeOperationFilterInput>;
  fixedLengthDataElements?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  lastModifiedAt?: InputMaybe<DateTimeOperationFilterInput>;
  lastModifiedByUserId?: InputMaybe<StringOperationFilterInput>;
  lastModifiedByUserName?: InputMaybe<StringOperationFilterInput>;
  leader?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<BibRecordFilterInput>>;
};

export type BibRecordSortInput = {
  catalogingSource?: InputMaybe<CatalogingSourceSortInput>;
  controlNumber?: InputMaybe<SortEnumType>;
  controlNumberIdentifier?: InputMaybe<SortEnumType>;
  createdAt?: InputMaybe<SortEnumType>;
  createdByUserId?: InputMaybe<SortEnumType>;
  createdByUserName?: InputMaybe<SortEnumType>;
  dateAndTimeOfLatestTransaction?: InputMaybe<SortEnumType>;
  fixedLengthDataElements?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  lastModifiedAt?: InputMaybe<SortEnumType>;
  lastModifiedByUserId?: InputMaybe<SortEnumType>;
  lastModifiedByUserName?: InputMaybe<SortEnumType>;
  leader?: InputMaybe<SortEnumType>;
};

/** A segment of a collection. */
export type BibRecordsCollectionSegment = {
  __typename?: 'BibRecordsCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<BibRecord>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

export type CatalogingSource = {
  __typename?: 'CatalogingSource';
  transcribingAgency: Scalars['String']['output'];
};

export type CatalogingSourceFilterInput = {
  and?: InputMaybe<Array<CatalogingSourceFilterInput>>;
  descriptionConventions?: InputMaybe<StringOperationFilterInput>;
  languageOfCataloging?: InputMaybe<StringOperationFilterInput>;
  modifyingAgency?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<CatalogingSourceFilterInput>>;
  originalCatalogingAgency?: InputMaybe<StringOperationFilterInput>;
  transcribingAgency?: InputMaybe<StringOperationFilterInput>;
};

export type CatalogingSourceInput = {
  descriptionConventions?: InputMaybe<Scalars['String']['input']>;
  languageOfCataloging?: InputMaybe<Scalars['String']['input']>;
  modifyingAgency?: InputMaybe<Scalars['String']['input']>;
  originalCatalogingAgency?: InputMaybe<Scalars['String']['input']>;
  transcribingAgency: Scalars['String']['input'];
};

export type CatalogingSourceSortInput = {
  descriptionConventions?: InputMaybe<SortEnumType>;
  languageOfCataloging?: InputMaybe<SortEnumType>;
  modifyingAgency?: InputMaybe<SortEnumType>;
  originalCatalogingAgency?: InputMaybe<SortEnumType>;
  transcribingAgency?: InputMaybe<SortEnumType>;
};

/** Information about the offset pagination. */
export type CollectionSegmentInfo = {
  __typename?: 'CollectionSegmentInfo';
  /** Indicates whether more items exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean']['output'];
  /** Indicates whether more items exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean']['output'];
};

export type CreateBibRecordInput = {
  catalogingSource: CatalogingSourceInput;
  controlNumber: Scalars['String']['input'];
  controlNumberIdentifier: Scalars['String']['input'];
  dateTimeOfLatestTransaction: Scalars['DateTime']['input'];
  fixedLengthDataElements: Scalars['String']['input'];
  leader: Scalars['String']['input'];
};

export type CreateBibRecordPayload = {
  __typename?: 'CreateBibRecordPayload';
  data?: Maybe<BibRecord>;
  errors?: Maybe<Array<ErrorType>>;
  message?: Maybe<Scalars['String']['output']>;
};

export type CreatePatronInput = {
  address?: InputMaybe<Scalars['String']['input']>;
  cardNumber: Scalars['String']['input'];
  contactNumber?: InputMaybe<Scalars['String']['input']>;
  dateOfBirth?: InputMaybe<Scalars['Date']['input']>;
  email?: InputMaybe<Scalars['String']['input']>;
  firstName?: InputMaybe<Scalars['String']['input']>;
  photoFileId?: InputMaybe<Scalars['String']['input']>;
  salutation?: InputMaybe<Scalars['String']['input']>;
  surname: Scalars['String']['input'];
};

export type CreatePatronPayload = {
  __typename?: 'CreatePatronPayload';
  data?: Maybe<Patron>;
  errors?: Maybe<Array<ErrorType>>;
  message?: Maybe<Scalars['String']['output']>;
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

export type DateTimeOperationFilterInput = {
  eq?: InputMaybe<Scalars['DateTime']['input']>;
  gt?: InputMaybe<Scalars['DateTime']['input']>;
  gte?: InputMaybe<Scalars['DateTime']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['DateTime']['input']>>>;
  lt?: InputMaybe<Scalars['DateTime']['input']>;
  lte?: InputMaybe<Scalars['DateTime']['input']>;
  neq?: InputMaybe<Scalars['DateTime']['input']>;
  ngt?: InputMaybe<Scalars['DateTime']['input']>;
  ngte?: InputMaybe<Scalars['DateTime']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['DateTime']['input']>>>;
  nlt?: InputMaybe<Scalars['DateTime']['input']>;
  nlte?: InputMaybe<Scalars['DateTime']['input']>;
};

export type DeletePatronInput = {
  id: Scalars['String']['input'];
};

export type DeletePatronPayload = {
  __typename?: 'DeletePatronPayload';
  errors?: Maybe<Array<ErrorType>>;
  message?: Maybe<Scalars['String']['output']>;
};

export type DeleteVendorInput = {
  id: Scalars['String']['input'];
};

export type DeleteVendorPayload = {
  __typename?: 'DeleteVendorPayload';
  errors?: Maybe<Array<ErrorType>>;
  message?: Maybe<Scalars['String']['output']>;
};

export type ErrorType = KnError | ValidationError;

export type KnError = {
  __typename?: 'KnError';
  code: Scalars['String']['output'];
  description?: Maybe<Scalars['String']['output']>;
  message: Scalars['String']['output'];
};

export type Mutation = {
  __typename?: 'Mutation';
  addVendor: AddVendorPayload;
  createBibRecord: CreateBibRecordPayload;
  createPatron: CreatePatronPayload;
  deletePatron: DeletePatronPayload;
  deleteVendor: DeleteVendorPayload;
  updatePatron: UpdatePatronPayload;
  updateVendor: UpdateVendorPayload;
};


export type MutationAddVendorArgs = {
  input: AddVendorInput;
};


export type MutationCreateBibRecordArgs = {
  input: CreateBibRecordInput;
};


export type MutationCreatePatronArgs = {
  input: CreatePatronInput;
};


export type MutationDeletePatronArgs = {
  input: DeletePatronInput;
};


export type MutationDeleteVendorArgs = {
  input: DeleteVendorInput;
};


export type MutationUpdatePatronArgs = {
  input: UpdatePatronInput;
};


export type MutationUpdateVendorArgs = {
  input: UpdateVendorInput;
};

export type Notification = {
  __typename?: 'Notification';
  message?: Maybe<Scalars['String']['output']>;
};

export type Patron = {
  __typename?: 'Patron';
  address?: Maybe<Scalars['String']['output']>;
  cardNumber: Scalars['String']['output'];
  contactNumber?: Maybe<Scalars['String']['output']>;
  email?: Maybe<Scalars['String']['output']>;
  firstName?: Maybe<Scalars['String']['output']>;
  fullName: Scalars['String']['output'];
  registrationDate: Scalars['Date']['output'];
  salutation?: Maybe<Scalars['String']['output']>;
  surname: Scalars['String']['output'];
};

export type PatronFilterInput = {
  address?: InputMaybe<StringOperationFilterInput>;
  and?: InputMaybe<Array<PatronFilterInput>>;
  cardNumber?: InputMaybe<StringOperationFilterInput>;
  contactNumber?: InputMaybe<StringOperationFilterInput>;
  createdAt?: InputMaybe<DateTimeOperationFilterInput>;
  createdByUserId?: InputMaybe<StringOperationFilterInput>;
  createdByUserName?: InputMaybe<StringOperationFilterInput>;
  dateOfBirth?: InputMaybe<DateOperationFilterInput>;
  email?: InputMaybe<StringOperationFilterInput>;
  firstName?: InputMaybe<StringOperationFilterInput>;
  fullName?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  lastModifiedAt?: InputMaybe<DateTimeOperationFilterInput>;
  lastModifiedByUserId?: InputMaybe<StringOperationFilterInput>;
  lastModifiedByUserName?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<PatronFilterInput>>;
  photoFileId?: InputMaybe<StringOperationFilterInput>;
  registrationDate?: InputMaybe<DateOperationFilterInput>;
  salutation?: InputMaybe<StringOperationFilterInput>;
  surname?: InputMaybe<StringOperationFilterInput>;
};

export type PatronPatchInput = {
  address?: InputMaybe<Scalars['String']['input']>;
  cardNumber?: InputMaybe<Scalars['String']['input']>;
  contactNumber?: InputMaybe<Scalars['String']['input']>;
  dateOfBirth?: InputMaybe<Scalars['Date']['input']>;
  email?: InputMaybe<Scalars['String']['input']>;
  firstName?: InputMaybe<Scalars['String']['input']>;
  photoFileId?: InputMaybe<Scalars['String']['input']>;
  salutation?: InputMaybe<Scalars['String']['input']>;
  surname?: InputMaybe<Scalars['String']['input']>;
};

export type PatronSortInput = {
  address?: InputMaybe<SortEnumType>;
  cardNumber?: InputMaybe<SortEnumType>;
  contactNumber?: InputMaybe<SortEnumType>;
  createdAt?: InputMaybe<SortEnumType>;
  createdByUserId?: InputMaybe<SortEnumType>;
  createdByUserName?: InputMaybe<SortEnumType>;
  dateOfBirth?: InputMaybe<SortEnumType>;
  email?: InputMaybe<SortEnumType>;
  firstName?: InputMaybe<SortEnumType>;
  fullName?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  lastModifiedAt?: InputMaybe<SortEnumType>;
  lastModifiedByUserId?: InputMaybe<SortEnumType>;
  lastModifiedByUserName?: InputMaybe<SortEnumType>;
  photoFileId?: InputMaybe<SortEnumType>;
  registrationDate?: InputMaybe<SortEnumType>;
  salutation?: InputMaybe<SortEnumType>;
  surname?: InputMaybe<SortEnumType>;
};

/** A segment of a collection. */
export type PatronsCollectionSegment = {
  __typename?: 'PatronsCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Patron>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

export type Query = {
  __typename?: 'Query';
  bibRecord?: Maybe<BibRecord>;
  bibRecords?: Maybe<BibRecordsCollectionSegment>;
  patron?: Maybe<Patron>;
  patrons?: Maybe<PatronsCollectionSegment>;
  vendor?: Maybe<Vendor>;
  vendors?: Maybe<VendorsCollectionSegment>;
};


export type QueryBibRecordArgs = {
  id: Scalars['String']['input'];
};


export type QueryBibRecordsArgs = {
  order?: InputMaybe<Array<BibRecordSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<BibRecordFilterInput>;
};


export type QueryPatronArgs = {
  id: Scalars['String']['input'];
};


export type QueryPatronsArgs = {
  order?: InputMaybe<Array<PatronSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<PatronFilterInput>;
};


export type QueryVendorArgs = {
  id: Scalars['String']['input'];
};


export type QueryVendorsArgs = {
  order?: InputMaybe<Array<VendorSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<VendorFilterInput>;
};

export type ResultOfPatron = {
  __typename?: 'ResultOfPatron';
  errors: Array<KnError>;
  isFailure: Scalars['Boolean']['output'];
  isSuccess: Scalars['Boolean']['output'];
  value: Patron;
};

export type ResultOfVendor = {
  __typename?: 'ResultOfVendor';
  errors: Array<KnError>;
  isFailure: Scalars['Boolean']['output'];
  isSuccess: Scalars['Boolean']['output'];
  value: Vendor;
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

export type UpdatePatronInput = {
  id: Scalars['String']['input'];
  patch: PatronPatchInput;
};

export type UpdatePatronPayload = {
  __typename?: 'UpdatePatronPayload';
  data?: Maybe<Patron>;
  errors?: Maybe<Array<ErrorType>>;
  message?: Maybe<Scalars['String']['output']>;
  result: ResultOfPatron;
};

export type UpdateVendorInput = {
  id: Scalars['String']['input'];
  patch: VendorPatchInput;
};

export type UpdateVendorPayload = {
  __typename?: 'UpdateVendorPayload';
  data?: Maybe<Vendor>;
  errors?: Maybe<Array<ErrorType>>;
  message?: Maybe<Scalars['String']['output']>;
  result: ResultOfVendor;
};

export type ValidationError = {
  __typename?: 'ValidationError';
  code: Scalars['String']['output'];
  description?: Maybe<Scalars['String']['output']>;
  fieldName: Scalars['String']['output'];
  message: Scalars['String']['output'];
};

export type Vendor = {
  __typename?: 'Vendor';
  accountDetail?: Maybe<Scalars['String']['output']>;
  address: Scalars['String']['output'];
  contactNumber: Scalars['String']['output'];
  contactPersonEmail?: Maybe<Scalars['String']['output']>;
  contactPersonName?: Maybe<Scalars['String']['output']>;
  contactPersonPhone?: Maybe<Scalars['String']['output']>;
  email?: Maybe<Scalars['String']['output']>;
  id: Scalars['String']['output'];
  status: VendorStatus;
  website?: Maybe<Scalars['String']['output']>;
};

export type VendorFilterInput = {
  accountDetail?: InputMaybe<StringOperationFilterInput>;
  address?: InputMaybe<StringOperationFilterInput>;
  and?: InputMaybe<Array<VendorFilterInput>>;
  contactNumber?: InputMaybe<StringOperationFilterInput>;
  contactPersonEmail?: InputMaybe<StringOperationFilterInput>;
  contactPersonName?: InputMaybe<StringOperationFilterInput>;
  contactPersonPhone?: InputMaybe<StringOperationFilterInput>;
  email?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<VendorFilterInput>>;
  status?: InputMaybe<VendorStatusOperationFilterInput>;
  website?: InputMaybe<StringOperationFilterInput>;
};

export type VendorPatchInput = {
  accountDetail?: InputMaybe<Scalars['String']['input']>;
  address?: InputMaybe<Scalars['String']['input']>;
  contactNumber?: InputMaybe<Scalars['String']['input']>;
  contactPersonEmail?: InputMaybe<Scalars['String']['input']>;
  contactPersonName?: InputMaybe<Scalars['String']['input']>;
  contactPersonPhone?: InputMaybe<Scalars['String']['input']>;
  email?: InputMaybe<Scalars['String']['input']>;
  name?: InputMaybe<Scalars['String']['input']>;
  status?: InputMaybe<VendorStatus>;
  website?: InputMaybe<Scalars['String']['input']>;
};

export type VendorSortInput = {
  accountDetail?: InputMaybe<SortEnumType>;
  address?: InputMaybe<SortEnumType>;
  contactNumber?: InputMaybe<SortEnumType>;
  contactPersonEmail?: InputMaybe<SortEnumType>;
  contactPersonName?: InputMaybe<SortEnumType>;
  contactPersonPhone?: InputMaybe<SortEnumType>;
  email?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  name?: InputMaybe<SortEnumType>;
  status?: InputMaybe<SortEnumType>;
  website?: InputMaybe<SortEnumType>;
};

export enum VendorStatus {
  Active = 'ACTIVE',
  Inactive = 'INACTIVE'
}

export type VendorStatusOperationFilterInput = {
  eq?: InputMaybe<VendorStatus>;
  in?: InputMaybe<Array<VendorStatus>>;
  neq?: InputMaybe<VendorStatus>;
  nin?: InputMaybe<Array<VendorStatus>>;
};

/** A segment of a collection. */
export type VendorsCollectionSegment = {
  __typename?: 'VendorsCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Vendor>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

export type GetVendorsQueryVariables = Exact<{
  skip: Scalars['Int']['input'];
  take: Scalars['Int']['input'];
  filter?: InputMaybe<VendorFilterInput>;
  sortBy?: InputMaybe<Array<VendorSortInput> | VendorSortInput>;
}>;


export type GetVendorsQuery = { __typename?: 'Query', vendors?: { __typename?: 'VendorsCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Vendor', id: string, contactNumber: string, email?: string | null, status: VendorStatus }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export const GetVendorsDocument = gql`
    query getVendors($skip: Int!, $take: Int!, $filter: VendorFilterInput, $sortBy: [VendorSortInput!]) {
  vendors(skip: $skip, take: $take, where: $filter, order: $sortBy) {
    items {
      id
      contactNumber
      email
      status
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
  export class GetVendorsGQL extends Apollo.Query<GetVendorsQuery, GetVendorsQueryVariables> {
    override document = GetVendorsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }