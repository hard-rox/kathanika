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
  /** The `Decimal` scalar type represents a decimal floating-point number. */
  Decimal: { input: any; output: any; }
  /** The `LocalDate` scalar type represents a ISO date string, represented as UTF-8 character sequences YYYY-MM-DD. The scalar follows the specification defined in RFC3339 */
  LocalDate: { input: any; output: any; }
  URL: { input: any; output: any; }
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
  controlNumber: Scalars['String']['output'];
  controlNumberIdentifier: Scalars['String']['output'];
  coverImageUrl?: Maybe<Scalars['URL']['output']>;
  dateAndTimeOfLatestTransaction: Scalars['String']['output'];
  fixedLengthDataElements: Scalars['String']['output'];
  id: Scalars['String']['output'];
  internationalStandardBookNumbers: Array<Scalars['String']['output']>;
  leader: Scalars['String']['output'];
  mainEntryPersonalName?: Maybe<MainEntryPersonalName>;
  physicalDescriptions: Array<PhysicalDescription>;
  publicationDistributions: Array<PublicationDistribution>;
  titleStatement: TitleStatement;
};

export type BibRecordFilterInput = {
  and?: InputMaybe<Array<BibRecordFilterInput>>;
  controlNumber?: InputMaybe<StringOperationFilterInput>;
  controlNumberIdentifier?: InputMaybe<StringOperationFilterInput>;
  coverImageId?: InputMaybe<StringOperationFilterInput>;
  dateAndTimeOfLatestTransaction?: InputMaybe<StringOperationFilterInput>;
  fixedLengthDataElements?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  internationalStandardBookNumbers?: InputMaybe<ListStringOperationFilterInput>;
  leader?: InputMaybe<StringOperationFilterInput>;
  mainEntryPersonalName?: InputMaybe<MainEntryPersonalNameFilterInput>;
  or?: InputMaybe<Array<BibRecordFilterInput>>;
  physicalDescriptions?: InputMaybe<ListFilterInputTypeOfPhysicalDescriptionFilterInput>;
  publicationDistributions?: InputMaybe<ListFilterInputTypeOfPublicationDistributionFilterInput>;
  titleStatement?: InputMaybe<TitleStatementFilterInput>;
};

export type BibRecordSortInput = {
  controlNumber?: InputMaybe<SortEnumType>;
  controlNumberIdentifier?: InputMaybe<SortEnumType>;
  coverImageId?: InputMaybe<SortEnumType>;
  dateAndTimeOfLatestTransaction?: InputMaybe<SortEnumType>;
  fixedLengthDataElements?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  leader?: InputMaybe<SortEnumType>;
  mainEntryPersonalName?: InputMaybe<MainEntryPersonalNameSortInput>;
  titleStatement?: InputMaybe<TitleStatementSortInput>;
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

/** Information about the offset pagination. */
export type CollectionSegmentInfo = {
  __typename?: 'CollectionSegmentInfo';
  /** Indicates whether more items exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean']['output'];
  /** Indicates whether more items exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean']['output'];
};

export type CreateBibRecordInput = {
  author?: InputMaybe<Scalars['String']['input']>;
  coverImageId?: InputMaybe<Scalars['String']['input']>;
  dimensions?: InputMaybe<Scalars['String']['input']>;
  extent?: InputMaybe<Scalars['String']['input']>;
  isbn?: InputMaybe<Scalars['String']['input']>;
  publicationDate?: InputMaybe<Scalars['String']['input']>;
  publisherName?: InputMaybe<Scalars['String']['input']>;
  title: Scalars['String']['input'];
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

export type CreatePurchaseOrderInput = {
  internalNote?: InputMaybe<Scalars['String']['input']>;
  items: Array<PurchaseItemInput>;
  vendorId: Scalars['String']['input'];
  vendorNote?: InputMaybe<Scalars['String']['input']>;
};

export type CreatePurchaseOrderPayload = {
  __typename?: 'CreatePurchaseOrderPayload';
  data?: Maybe<PurchaseOrder>;
  errors?: Maybe<Array<ErrorType>>;
  message?: Maybe<Scalars['String']['output']>;
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

export type KnError = {
  __typename?: 'KnError';
  code: Scalars['String']['output'];
  description?: Maybe<Scalars['String']['output']>;
  message: Scalars['String']['output'];
};

export type ListFilterInputTypeOfPhysicalDescriptionFilterInput = {
  all?: InputMaybe<PhysicalDescriptionFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<PhysicalDescriptionFilterInput>;
  some?: InputMaybe<PhysicalDescriptionFilterInput>;
};

export type ListFilterInputTypeOfPublicationDistributionFilterInput = {
  all?: InputMaybe<PublicationDistributionFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<PublicationDistributionFilterInput>;
  some?: InputMaybe<PublicationDistributionFilterInput>;
};

export type ListFilterInputTypeOfPurchaseItemFilterInput = {
  all?: InputMaybe<PurchaseItemFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<PurchaseItemFilterInput>;
  some?: InputMaybe<PurchaseItemFilterInput>;
};

export type ListStringOperationFilterInput = {
  all?: InputMaybe<StringOperationFilterInput>;
  any?: InputMaybe<Scalars['Boolean']['input']>;
  none?: InputMaybe<StringOperationFilterInput>;
  some?: InputMaybe<StringOperationFilterInput>;
};

export type LocalDateOperationFilterInput = {
  eq?: InputMaybe<Scalars['LocalDate']['input']>;
  gt?: InputMaybe<Scalars['LocalDate']['input']>;
  gte?: InputMaybe<Scalars['LocalDate']['input']>;
  in?: InputMaybe<Array<InputMaybe<Scalars['LocalDate']['input']>>>;
  lt?: InputMaybe<Scalars['LocalDate']['input']>;
  lte?: InputMaybe<Scalars['LocalDate']['input']>;
  neq?: InputMaybe<Scalars['LocalDate']['input']>;
  ngt?: InputMaybe<Scalars['LocalDate']['input']>;
  ngte?: InputMaybe<Scalars['LocalDate']['input']>;
  nin?: InputMaybe<Array<InputMaybe<Scalars['LocalDate']['input']>>>;
  nlt?: InputMaybe<Scalars['LocalDate']['input']>;
  nlte?: InputMaybe<Scalars['LocalDate']['input']>;
};

export type MainEntryPersonalName = {
  __typename?: 'MainEntryPersonalName';
  personalName: Scalars['String']['output'];
  relatorTerms: Array<Scalars['String']['output']>;
};

export type MainEntryPersonalNameFilterInput = {
  and?: InputMaybe<Array<MainEntryPersonalNameFilterInput>>;
  or?: InputMaybe<Array<MainEntryPersonalNameFilterInput>>;
  personalName?: InputMaybe<StringOperationFilterInput>;
  relatorTerms?: InputMaybe<ListStringOperationFilterInput>;
};

export type MainEntryPersonalNameSortInput = {
  personalName?: InputMaybe<SortEnumType>;
};

export type Mutation = {
  __typename?: 'Mutation';
  addVendor: AddVendorPayload;
  createBibRecord: CreateBibRecordPayload;
  createPatron: CreatePatronPayload;
  createPurchaseOrder: CreatePurchaseOrderPayload;
  deletePatron: DeletePatronPayload;
  deleteVendor: DeleteVendorPayload;
  updatePatron: UpdatePatronPayload;
  updatePurchaseOrder: UpdatePurchaseOrderPayload;
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


export type MutationCreatePurchaseOrderArgs = {
  input: CreatePurchaseOrderInput;
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


export type MutationUpdatePurchaseOrderArgs = {
  input: UpdatePurchaseOrderInput;
};


export type MutationUpdateVendorArgs = {
  input: UpdateVendorInput;
};

export type Patron = {
  __typename?: 'Patron';
  address?: Maybe<Scalars['String']['output']>;
  cardNumber: Scalars['String']['output'];
  contactNumber?: Maybe<Scalars['String']['output']>;
  dateOfBirth?: Maybe<Scalars['Date']['output']>;
  email?: Maybe<Scalars['String']['output']>;
  firstName?: Maybe<Scalars['String']['output']>;
  fullName: Scalars['String']['output'];
  id: Scalars['String']['output'];
  photoFileId?: Maybe<Scalars['String']['output']>;
  registrationDate: Scalars['Date']['output'];
  salutation?: Maybe<Scalars['String']['output']>;
  surname: Scalars['String']['output'];
};

export type PatronFilterInput = {
  address?: InputMaybe<StringOperationFilterInput>;
  and?: InputMaybe<Array<PatronFilterInput>>;
  cardNumber?: InputMaybe<StringOperationFilterInput>;
  contactNumber?: InputMaybe<StringOperationFilterInput>;
  dateOfBirth?: InputMaybe<LocalDateOperationFilterInput>;
  email?: InputMaybe<StringOperationFilterInput>;
  firstName?: InputMaybe<StringOperationFilterInput>;
  fullName?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<PatronFilterInput>>;
  photoFileId?: InputMaybe<StringOperationFilterInput>;
  registrationDate?: InputMaybe<LocalDateOperationFilterInput>;
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
  dateOfBirth?: InputMaybe<SortEnumType>;
  email?: InputMaybe<SortEnumType>;
  firstName?: InputMaybe<SortEnumType>;
  fullName?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
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

export type PhysicalDescription = {
  __typename?: 'PhysicalDescription';
  dimensions: Array<Scalars['String']['output']>;
  extents: Array<Scalars['String']['output']>;
};

export type PhysicalDescriptionFilterInput = {
  and?: InputMaybe<Array<PhysicalDescriptionFilterInput>>;
  dimensions?: InputMaybe<ListStringOperationFilterInput>;
  extents?: InputMaybe<ListStringOperationFilterInput>;
  or?: InputMaybe<Array<PhysicalDescriptionFilterInput>>;
};

export type PublicationDistribution = {
  __typename?: 'PublicationDistribution';
  datesOfPublication: Array<Scalars['String']['output']>;
  namesOfPublisher: Array<Scalars['String']['output']>;
  placesOfPublication: Array<Scalars['String']['output']>;
};

export type PublicationDistributionFilterInput = {
  and?: InputMaybe<Array<PublicationDistributionFilterInput>>;
  datesOfPublication?: InputMaybe<ListStringOperationFilterInput>;
  namesOfPublisher?: InputMaybe<ListStringOperationFilterInput>;
  or?: InputMaybe<Array<PublicationDistributionFilterInput>>;
  placesOfPublication?: InputMaybe<ListStringOperationFilterInput>;
};

export type PurchaseItem = {
  __typename?: 'PurchaseItem';
  author?: Maybe<Scalars['String']['output']>;
  edition?: Maybe<Scalars['String']['output']>;
  id: Scalars['String']['output'];
  internalNote?: Maybe<Scalars['String']['output']>;
  isbn?: Maybe<Scalars['String']['output']>;
  publisher?: Maybe<Scalars['String']['output']>;
  publishingYear?: Maybe<Scalars['Int']['output']>;
  quantity: Scalars['Int']['output'];
  title: Scalars['String']['output'];
  totalCost: Scalars['Decimal']['output'];
  vendorNote?: Maybe<Scalars['String']['output']>;
  vendorPrice?: Maybe<Scalars['Decimal']['output']>;
};

export type PurchaseItemFilterInput = {
  and?: InputMaybe<Array<PurchaseItemFilterInput>>;
  author?: InputMaybe<StringOperationFilterInput>;
  edition?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  internalNote?: InputMaybe<StringOperationFilterInput>;
  isbn?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<PurchaseItemFilterInput>>;
  publisher?: InputMaybe<StringOperationFilterInput>;
  publishingYear?: InputMaybe<IntOperationFilterInput>;
  quantity?: InputMaybe<IntOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
  totalCost?: InputMaybe<DecimalOperationFilterInput>;
  vendorNote?: InputMaybe<StringOperationFilterInput>;
  vendorPrice?: InputMaybe<DecimalOperationFilterInput>;
};

export type PurchaseItemInput = {
  author?: InputMaybe<Scalars['String']['input']>;
  edition?: InputMaybe<Scalars['String']['input']>;
  internalNote?: InputMaybe<Scalars['String']['input']>;
  isbn?: InputMaybe<Scalars['String']['input']>;
  publisher?: InputMaybe<Scalars['String']['input']>;
  publishingYear?: InputMaybe<Scalars['Int']['input']>;
  quantity: Scalars['Int']['input'];
  title: Scalars['String']['input'];
  vendorNote?: InputMaybe<Scalars['String']['input']>;
  vendorPrice?: InputMaybe<Scalars['Decimal']['input']>;
};

export type PurchaseOrder = {
  __typename?: 'PurchaseOrder';
  id: Scalars['String']['output'];
  internalNote?: Maybe<Scalars['String']['output']>;
  orderDate: Scalars['Date']['output'];
  purchaseItems: Array<PurchaseItem>;
  status: PurchaseOrderStatus;
  totalCost: Scalars['Decimal']['output'];
  totalQuantity: Scalars['Int']['output'];
  vendorId: Scalars['String']['output'];
  vendorName: Scalars['String']['output'];
  vendorNote?: Maybe<Scalars['String']['output']>;
};

export type PurchaseOrderFilterInput = {
  and?: InputMaybe<Array<PurchaseOrderFilterInput>>;
  id?: InputMaybe<StringOperationFilterInput>;
  internalNote?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<PurchaseOrderFilterInput>>;
  orderDate?: InputMaybe<LocalDateOperationFilterInput>;
  purchaseItems?: InputMaybe<ListFilterInputTypeOfPurchaseItemFilterInput>;
  status?: InputMaybe<PurchaseOrderStatusOperationFilterInput>;
  totalCost?: InputMaybe<DecimalOperationFilterInput>;
  totalQuantity?: InputMaybe<IntOperationFilterInput>;
  vendorId?: InputMaybe<StringOperationFilterInput>;
  vendorName?: InputMaybe<StringOperationFilterInput>;
  vendorNote?: InputMaybe<StringOperationFilterInput>;
};

export type PurchaseOrderPatchInput = {
  internalNote?: InputMaybe<Scalars['String']['input']>;
  vendorId?: InputMaybe<Scalars['String']['input']>;
  vendorNote?: InputMaybe<Scalars['String']['input']>;
};

export type PurchaseOrderSortInput = {
  id?: InputMaybe<SortEnumType>;
  internalNote?: InputMaybe<SortEnumType>;
  orderDate?: InputMaybe<SortEnumType>;
  status?: InputMaybe<SortEnumType>;
  totalCost?: InputMaybe<SortEnumType>;
  totalQuantity?: InputMaybe<SortEnumType>;
  vendorId?: InputMaybe<SortEnumType>;
  vendorName?: InputMaybe<SortEnumType>;
  vendorNote?: InputMaybe<SortEnumType>;
};

export enum PurchaseOrderStatus {
  Completed = 'COMPLETED',
  Pending = 'PENDING'
}

export type PurchaseOrderStatusOperationFilterInput = {
  eq?: InputMaybe<PurchaseOrderStatus>;
  in?: InputMaybe<Array<PurchaseOrderStatus>>;
  neq?: InputMaybe<PurchaseOrderStatus>;
  nin?: InputMaybe<Array<PurchaseOrderStatus>>;
};

/** A segment of a collection. */
export type PurchaseOrdersCollectionSegment = {
  __typename?: 'PurchaseOrdersCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<PurchaseOrder>>;
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
  purchaseOrder?: Maybe<PurchaseOrder>;
  purchaseOrders?: Maybe<PurchaseOrdersCollectionSegment>;
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


export type QueryPurchaseOrderArgs = {
  id: Scalars['String']['input'];
};


export type QueryPurchaseOrdersArgs = {
  order?: InputMaybe<Array<PurchaseOrderSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<PurchaseOrderFilterInput>;
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

export type TitleStatement = {
  __typename?: 'TitleStatement';
  remainderOfTitle?: Maybe<Scalars['String']['output']>;
  statementOfResponsibility?: Maybe<Scalars['String']['output']>;
  title: Scalars['String']['output'];
};

export type TitleStatementFilterInput = {
  and?: InputMaybe<Array<TitleStatementFilterInput>>;
  or?: InputMaybe<Array<TitleStatementFilterInput>>;
  remainderOfTitle?: InputMaybe<StringOperationFilterInput>;
  statementOfResponsibility?: InputMaybe<StringOperationFilterInput>;
  title?: InputMaybe<StringOperationFilterInput>;
};

export type TitleStatementSortInput = {
  remainderOfTitle?: InputMaybe<SortEnumType>;
  statementOfResponsibility?: InputMaybe<SortEnumType>;
  title?: InputMaybe<SortEnumType>;
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
};

export type UpdatePurchaseOrderInput = {
  id: Scalars['String']['input'];
  patch: PurchaseOrderPatchInput;
};

export type UpdatePurchaseOrderPayload = {
  __typename?: 'UpdatePurchaseOrderPayload';
  data?: Maybe<PurchaseOrder>;
  errors?: Maybe<Array<ErrorType>>;
  message?: Maybe<Scalars['String']['output']>;
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
  name: Scalars['String']['output'];
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

export type CreateBibRecordMutationVariables = Exact<{
  input: CreateBibRecordInput;
}>;


export type CreateBibRecordMutation = { __typename?: 'Mutation', createBibRecord: { __typename?: 'CreateBibRecordPayload', message?: string | null, data?: { __typename?: 'BibRecord', id: string, titleStatement: { __typename?: 'TitleStatement', title: string } } | null, errors?: Array<{ __typename?: 'KnError', code: string, message: string, description?: string | null } | { __typename?: 'ValidationError', code: string, fieldName: string, message: string, description?: string | null }> | null } };

export type BibRecordDetailsQueryVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type BibRecordDetailsQuery = { __typename?: 'Query', bibRecord?: { __typename?: 'BibRecord', id: string, internationalStandardBookNumbers: Array<string>, coverImageUrl?: any | null, titleStatement: { __typename?: 'TitleStatement', title: string, statementOfResponsibility?: string | null }, mainEntryPersonalName?: { __typename?: 'MainEntryPersonalName', personalName: string } | null, publicationDistributions: Array<{ __typename?: 'PublicationDistribution', namesOfPublisher: Array<string>, datesOfPublication: Array<string> }>, physicalDescriptions: Array<{ __typename?: 'PhysicalDescription', extents: Array<string>, dimensions: Array<string> }> } | null };

export type BibRecordListQueryVariables = Exact<{
  skip: Scalars['Int']['input'];
  take: Scalars['Int']['input'];
  filter?: InputMaybe<BibRecordFilterInput>;
  sortBy?: InputMaybe<Array<BibRecordSortInput> | BibRecordSortInput>;
}>;


export type BibRecordListQuery = { __typename?: 'Query', bibRecords?: { __typename?: 'BibRecordsCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'BibRecord', id: string, titleStatement: { __typename?: 'TitleStatement', title: string }, publicationDistributions: Array<{ __typename?: 'PublicationDistribution', namesOfPublisher: Array<string>, datesOfPublication: Array<string> }> }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type CreatePurchaseOrderMutationVariables = Exact<{
  input: CreatePurchaseOrderInput;
}>;


export type CreatePurchaseOrderMutation = { __typename?: 'Mutation', createPurchaseOrder: { __typename?: 'CreatePurchaseOrderPayload', message?: string | null, data?: { __typename?: 'PurchaseOrder', id: string, vendorName: string } | null, errors?: Array<{ __typename?: 'KnError', code: string, message: string, description?: string | null } | { __typename?: 'ValidationError', code: string, fieldName: string, message: string, description?: string | null }> | null } };

export type PurchaseOrderDetailsQueryVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type PurchaseOrderDetailsQuery = { __typename?: 'Query', purchaseOrder?: { __typename?: 'PurchaseOrder', id: string, orderDate: any, vendorName: string, internalNote?: string | null, vendorNote?: string | null, totalQuantity: number, totalCost: any, status: PurchaseOrderStatus, purchaseItems: Array<{ __typename?: 'PurchaseItem', id: string, title: string, author?: string | null, edition?: string | null, publisher?: string | null, publishingYear?: number | null, internalNote?: string | null, vendorNote?: string | null, quantity: number, vendorPrice?: any | null, totalCost: any }> } | null };

export type SearchVendorsQueryVariables = Exact<{
  searchTerm: Scalars['String']['input'];
}>;


export type SearchVendorsQuery = { __typename?: 'Query', vendors?: { __typename?: 'VendorsCollectionSegment', items?: Array<{ __typename?: 'Vendor', id: string, name: string }> | null } | null };

export type PurchaseOrderListQueryVariables = Exact<{
  skip: Scalars['Int']['input'];
  take: Scalars['Int']['input'];
  filter?: InputMaybe<PurchaseOrderFilterInput>;
  sortBy?: InputMaybe<Array<PurchaseOrderSortInput> | PurchaseOrderSortInput>;
}>;


export type PurchaseOrderListQuery = { __typename?: 'Query', purchaseOrders?: { __typename?: 'PurchaseOrdersCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'PurchaseOrder', id: string, vendorName: string, totalQuantity: number, totalCost: any, status: PurchaseOrderStatus }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type AddVendorMutationVariables = Exact<{
  input: AddVendorInput;
}>;


export type AddVendorMutation = { __typename?: 'Mutation', addVendor: { __typename?: 'AddVendorPayload', message?: string | null, data?: { __typename?: 'Vendor', id: string, name: string } | null, errors?: Array<{ __typename?: 'KnError', code: string, message: string, description?: string | null } | { __typename?: 'ValidationError', code: string, fieldName: string, message: string, description?: string | null }> | null } };

export type VendorDetailsQueryVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type VendorDetailsQuery = { __typename?: 'Query', vendor?: { __typename?: 'Vendor', id: string, name: string, contactNumber: string, address: string, email?: string | null, status: VendorStatus, contactPersonName?: string | null, contactPersonPhone?: string | null, contactPersonEmail?: string | null, accountDetail?: string | null, website?: string | null } | null, latestPurchaseOrders?: { __typename?: 'PurchaseOrdersCollectionSegment', items?: Array<{ __typename?: 'PurchaseOrder', id: string, orderDate: any, totalCost: any, status: PurchaseOrderStatus }> | null } | null };

export type DeleteVendorMutationVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type DeleteVendorMutation = { __typename?: 'Mutation', deleteVendor: { __typename?: 'DeleteVendorPayload', message?: string | null, errors?: Array<{ __typename?: 'KnError', code: string, message: string, description?: string | null } | { __typename?: 'ValidationError', code: string, fieldName: string, message: string, description?: string | null }> | null } };

export type VendorListQueryVariables = Exact<{
  skip: Scalars['Int']['input'];
  take: Scalars['Int']['input'];
  filter?: InputMaybe<VendorFilterInput>;
  sortBy?: InputMaybe<Array<VendorSortInput> | VendorSortInput>;
}>;


export type VendorListQuery = { __typename?: 'Query', vendors?: { __typename?: 'VendorsCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Vendor', id: string, name: string, status: VendorStatus, contactPersonName?: string | null }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type UpdateVendorMutationVariables = Exact<{
  id: Scalars['String']['input'];
  patch: VendorPatchInput;
}>;


export type UpdateVendorMutation = { __typename?: 'Mutation', updateVendor: { __typename?: 'UpdateVendorPayload', message?: string | null, data?: { __typename?: 'Vendor', id: string, name: string } | null, errors?: Array<{ __typename?: 'KnError', code: string, message: string, description?: string | null } | { __typename?: 'ValidationError', code: string, fieldName: string, message: string, description?: string | null }> | null } };

export const CreateBibRecordDocument = gql`
    mutation createBibRecord($input: CreateBibRecordInput!) {
  createBibRecord(input: $input) {
    data {
      id
      titleStatement {
        title
      }
    }
    errors {
      ... on KnError {
        code
        message
        description
      }
      ... on ValidationError {
        code
        fieldName
        message
        description
      }
    }
    message
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class CreateBibRecordGQL extends Apollo.Mutation<CreateBibRecordMutation, CreateBibRecordMutationVariables> {
    override document = CreateBibRecordDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const BibRecordDetailsDocument = gql`
    query BibRecordDetails($id: String!) {
  bibRecord(id: $id) {
    id
    titleStatement {
      title
      statementOfResponsibility
    }
    internationalStandardBookNumbers
    mainEntryPersonalName {
      personalName
    }
    publicationDistributions {
      namesOfPublisher
      datesOfPublication
    }
    physicalDescriptions {
      extents
      dimensions
    }
    coverImageUrl
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class BibRecordDetailsGQL extends Apollo.Query<BibRecordDetailsQuery, BibRecordDetailsQueryVariables> {
    override document = BibRecordDetailsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const BibRecordListDocument = gql`
    query bibRecordList($skip: Int!, $take: Int!, $filter: BibRecordFilterInput, $sortBy: [BibRecordSortInput!]) {
  bibRecords(skip: $skip, take: $take, where: $filter, order: $sortBy) {
    items {
      id
      titleStatement {
        title
      }
      publicationDistributions {
        namesOfPublisher
        datesOfPublication
      }
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
  export class BibRecordListGQL extends Apollo.Query<BibRecordListQuery, BibRecordListQueryVariables> {
    override document = BibRecordListDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const CreatePurchaseOrderDocument = gql`
    mutation createPurchaseOrder($input: CreatePurchaseOrderInput!) {
  createPurchaseOrder(input: $input) {
    data {
      id
      vendorName
    }
    errors {
      ... on KnError {
        code
        message
        description
      }
      ... on ValidationError {
        code
        fieldName
        message
        description
      }
    }
    message
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class CreatePurchaseOrderGQL extends Apollo.Mutation<CreatePurchaseOrderMutation, CreatePurchaseOrderMutationVariables> {
    override document = CreatePurchaseOrderDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const PurchaseOrderDetailsDocument = gql`
    query purchaseOrderDetails($id: String!) {
  purchaseOrder(id: $id) {
    id
    orderDate
    vendorName
    internalNote
    vendorNote
    totalQuantity
    totalCost
    status
    purchaseItems {
      id
      title
      author
      edition
      publisher
      publishingYear
      internalNote
      vendorNote
      quantity
      vendorPrice
      totalCost
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class PurchaseOrderDetailsGQL extends Apollo.Query<PurchaseOrderDetailsQuery, PurchaseOrderDetailsQueryVariables> {
    override document = PurchaseOrderDetailsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const SearchVendorsDocument = gql`
    query searchVendors($searchTerm: String!) {
  vendors(
    skip: 0
    take: 100
    order: {name: ASC}
    where: {and: [{status: {eq: ACTIVE}}, {or: [{name: {contains: $searchTerm}}]}]}
  ) {
    items {
      id
      name
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class SearchVendorsGQL extends Apollo.Query<SearchVendorsQuery, SearchVendorsQueryVariables> {
    override document = SearchVendorsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const PurchaseOrderListDocument = gql`
    query purchaseOrderList($skip: Int!, $take: Int!, $filter: PurchaseOrderFilterInput, $sortBy: [PurchaseOrderSortInput!]) {
  purchaseOrders(skip: $skip, take: $take, where: $filter, order: $sortBy) {
    items {
      id
      vendorName
      totalQuantity
      totalCost
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
  export class PurchaseOrderListGQL extends Apollo.Query<PurchaseOrderListQuery, PurchaseOrderListQueryVariables> {
    override document = PurchaseOrderListDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const AddVendorDocument = gql`
    mutation addVendor($input: AddVendorInput!) {
  addVendor(input: $input) {
    data {
      id
      name
    }
    errors {
      ... on KnError {
        code
        message
        description
      }
      ... on ValidationError {
        code
        fieldName
        message
        description
      }
    }
    message
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class AddVendorGQL extends Apollo.Mutation<AddVendorMutation, AddVendorMutationVariables> {
    override document = AddVendorDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const VendorDetailsDocument = gql`
    query vendorDetails($id: String!) {
  vendor(id: $id) {
    id
    name
    contactNumber
    address
    email
    status
    contactPersonName
    contactPersonPhone
    contactPersonEmail
    accountDetail
    website
  }
  latestPurchaseOrders: purchaseOrders(
    skip: 0
    take: 10
    where: {vendorId: {eq: $id}}
    order: {id: DESC}
  ) {
    items {
      id
      orderDate
      totalCost
      status
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class VendorDetailsGQL extends Apollo.Query<VendorDetailsQuery, VendorDetailsQueryVariables> {
    override document = VendorDetailsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const DeleteVendorDocument = gql`
    mutation deleteVendor($id: String!) {
  deleteVendor(input: {id: $id}) {
    errors {
      ... on KnError {
        code
        message
        description
      }
      ... on ValidationError {
        code
        fieldName
        message
        description
      }
    }
    message
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class DeleteVendorGQL extends Apollo.Mutation<DeleteVendorMutation, DeleteVendorMutationVariables> {
    override document = DeleteVendorDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const VendorListDocument = gql`
    query vendorList($skip: Int!, $take: Int!, $filter: VendorFilterInput, $sortBy: [VendorSortInput!]) {
  vendors(skip: $skip, take: $take, where: $filter, order: $sortBy) {
    items {
      id
      name
      status
      contactPersonName
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
  export class VendorListGQL extends Apollo.Query<VendorListQuery, VendorListQueryVariables> {
    override document = VendorListDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const UpdateVendorDocument = gql`
    mutation updateVendor($id: String!, $patch: VendorPatchInput!) {
  updateVendor(input: {id: $id, patch: $patch}) {
    data {
      id
      name
    }
    errors {
      ... on KnError {
        code
        message
        description
      }
      ... on ValidationError {
        code
        fieldName
        message
        description
      }
    }
    message
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class UpdateVendorGQL extends Apollo.Mutation<UpdateVendorMutation, UpdateVendorMutationVariables> {
    override document = UpdateVendorDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }