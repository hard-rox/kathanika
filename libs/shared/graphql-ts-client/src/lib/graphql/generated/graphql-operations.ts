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
  markedAsDeceased?: Scalars['Boolean']['input'];
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
  edition: Scalars['String']['input'];
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

export type AddPublisherError = InvalidFieldError;

export type AddPublisherInput = {
  contactInformation?: InputMaybe<Scalars['String']['input']>;
  description?: InputMaybe<Scalars['String']['input']>;
  name: Scalars['String']['input'];
};

export type AddPublisherPayload = {
  __typename?: 'AddPublisherPayload';
  data?: Maybe<Publisher>;
  errors?: Maybe<Array<AddPublisherError>>;
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
  dateOfDeath?: InputMaybe<Scalars['Date']['input']>;
  firstName?: InputMaybe<Scalars['String']['input']>;
  lastName?: InputMaybe<Scalars['String']['input']>;
  markedAsDeceased?: Scalars['Boolean']['input'];
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

export type CancelMembershipError = InvalidFieldError;

export type CancelMembershipInput = {
  id: Scalars['String']['input'];
};

export type CancelMembershipPayload = {
  __typename?: 'CancelMembershipPayload';
  errors?: Maybe<Array<CancelMembershipError>>;
  message?: Maybe<Scalars['String']['output']>;
};

/** Information about the offset pagination. */
export type CollectionSegmentInfo = {
  __typename?: 'CollectionSegmentInfo';
  /** Indicates whether more items exist following the set defined by the clients arguments. */
  hasNextPage: Scalars['Boolean']['output'];
  /** Indicates whether more items exist prior the set defined by the clients arguments. */
  hasPreviousPage: Scalars['Boolean']['output'];
};

export type CreateMemberError = InvalidFieldError;

export type CreateMemberInput = {
  address: Scalars['String']['input'];
  contactNumber: Scalars['String']['input'];
  dateOfBirth: Scalars['Date']['input'];
  email: Scalars['String']['input'];
  firstName: Scalars['String']['input'];
  lastName: Scalars['String']['input'];
};

export type CreateMemberPayload = {
  __typename?: 'CreateMemberPayload';
  data?: Maybe<Member>;
  errors?: Maybe<Array<CreateMemberError>>;
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

export type DeletePublisherError = DeletionFailedError | NotFoundWithTheIdError;

export type DeletePublisherInput = {
  id: Scalars['String']['input'];
};

export type DeletePublisherPayload = {
  __typename?: 'DeletePublisherPayload';
  errors?: Maybe<Array<DeletePublisherError>>;
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

export type Member = {
  __typename?: 'Member';
  address: Scalars['String']['output'];
  contactNumber: Scalars['String']['output'];
  dateOfBirth: Scalars['Date']['output'];
  email: Scalars['String']['output'];
  firstName: Scalars['String']['output'];
  fullName: Scalars['String']['output'];
  id: Scalars['String']['output'];
  lastName: Scalars['String']['output'];
  membershipStartDateTime: Scalars['DateTime']['output'];
  status: MembershipStatus;
};

export type MemberFilterInput = {
  address?: InputMaybe<StringOperationFilterInput>;
  and?: InputMaybe<Array<MemberFilterInput>>;
  contactNumber?: InputMaybe<StringOperationFilterInput>;
  dateOfBirth?: InputMaybe<DateOperationFilterInput>;
  email?: InputMaybe<StringOperationFilterInput>;
  firstName?: InputMaybe<StringOperationFilterInput>;
  fullName?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  lastMembershipSuspensionDateTime?: InputMaybe<DateTimeOperationFilterInput>;
  lastName?: InputMaybe<StringOperationFilterInput>;
  membershipCancellationDateTime?: InputMaybe<DateTimeOperationFilterInput>;
  membershipStartDateTime?: InputMaybe<DateTimeOperationFilterInput>;
  or?: InputMaybe<Array<MemberFilterInput>>;
  status?: InputMaybe<MembershipStatusOperationFilterInput>;
};

export type MemberPatchInput = {
  address?: InputMaybe<Scalars['String']['input']>;
  contactNumber?: InputMaybe<Scalars['String']['input']>;
  dateOfBirth?: InputMaybe<Scalars['Date']['input']>;
  email?: InputMaybe<Scalars['String']['input']>;
  firstName?: InputMaybe<Scalars['String']['input']>;
  lastName?: InputMaybe<Scalars['String']['input']>;
};

export type MemberSortInput = {
  address?: InputMaybe<SortEnumType>;
  contactNumber?: InputMaybe<SortEnumType>;
  dateOfBirth?: InputMaybe<SortEnumType>;
  email?: InputMaybe<SortEnumType>;
  firstName?: InputMaybe<SortEnumType>;
  fullName?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  lastMembershipSuspensionDateTime?: InputMaybe<SortEnumType>;
  lastName?: InputMaybe<SortEnumType>;
  membershipCancellationDateTime?: InputMaybe<SortEnumType>;
  membershipStartDateTime?: InputMaybe<SortEnumType>;
  status?: InputMaybe<SortEnumType>;
};

/** A segment of a collection. */
export type MembersCollectionSegment = {
  __typename?: 'MembersCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Member>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

export enum MembershipStatus {
  Active = 'ACTIVE',
  Cancelled = 'CANCELLED',
  Suspended = 'SUSPENDED'
}

export type MembershipStatusOperationFilterInput = {
  eq?: InputMaybe<MembershipStatus>;
  in?: InputMaybe<Array<MembershipStatus>>;
  neq?: InputMaybe<MembershipStatus>;
  nin?: InputMaybe<Array<MembershipStatus>>;
};

export type Mutations = {
  __typename?: 'Mutations';
  addAuthor: AddAuthorPayload;
  addPublication: AddPublicationPayload;
  addPublisher: AddPublisherPayload;
  cancelMembership: CancelMembershipPayload;
  createMember: CreateMemberPayload;
  deleteAuthor: DeleteAuthorPayload;
  deletePublisher: DeletePublisherPayload;
  renewMembership: RenewMembershipPayload;
  suspendMembership: SuspendMembershipPayload;
  updateAuthor: UpdateAuthorPayload;
  updateMember: UpdateMemberPayload;
  updatePublication: UpdatePublicationPayload;
  updatePublisher: UpdatePublisherPayload;
};


export type MutationsAddAuthorArgs = {
  input: AddAuthorInput;
};


export type MutationsAddPublicationArgs = {
  input: AddPublicationInput;
};


export type MutationsAddPublisherArgs = {
  input: AddPublisherInput;
};


export type MutationsCancelMembershipArgs = {
  input: CancelMembershipInput;
};


export type MutationsCreateMemberArgs = {
  input: CreateMemberInput;
};


export type MutationsDeleteAuthorArgs = {
  input: DeleteAuthorInput;
};


export type MutationsDeletePublisherArgs = {
  input: DeletePublisherInput;
};


export type MutationsRenewMembershipArgs = {
  input: RenewMembershipInput;
};


export type MutationsSuspendMembershipArgs = {
  input: SuspendMembershipInput;
};


export type MutationsUpdateAuthorArgs = {
  input: UpdateAuthorInput;
};


export type MutationsUpdateMemberArgs = {
  input: UpdateMemberInput;
};


export type MutationsUpdatePublicationArgs = {
  input: UpdatePublicationInput;
};


export type MutationsUpdatePublisherArgs = {
  input: UpdatePublisherInput;
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
  fullName: Scalars['String']['output'];
  id: Scalars['String']['output'];
  lastName: Scalars['String']['output'];
};

export type PublicationAuthorFilterInput = {
  and?: InputMaybe<Array<PublicationAuthorFilterInput>>;
  firstName?: InputMaybe<StringOperationFilterInput>;
  fullName?: InputMaybe<StringOperationFilterInput>;
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
  edition?: InputMaybe<Scalars['String']['input']>;
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
  AcademicPaper = 'ACADEMIC_PAPER',
  Blog = 'BLOG',
  Book = 'BOOK',
  Brochure = 'BROCHURE',
  CaseStudy = 'CASE_STUDY',
  Catalog = 'CATALOG',
  ConferenceProceedings = 'CONFERENCE_PROCEEDINGS',
  Directory = 'DIRECTORY',
  EBook = 'E_BOOK',
  GraphicNovel = 'GRAPHIC_NOVEL',
  Journal = 'JOURNAL',
  Magalogs = 'MAGALOGS',
  Magazine = 'MAGAZINE',
  Manual = 'MANUAL',
  Newsletter = 'NEWSLETTER',
  Newspaper = 'NEWSPAPER',
  Podcast = 'PODCAST',
  PressRelease = 'PRESS_RELEASE',
  Report = 'REPORT',
  Screenplay = 'SCREENPLAY',
  SocialMedia = 'SOCIAL_MEDIA',
  Thesis = 'THESIS',
  Website = 'WEBSITE',
  WhitePaper = 'WHITE_PAPER',
  Zine = 'ZINE'
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

export type Publisher = {
  __typename?: 'Publisher';
  contactInformation?: Maybe<Scalars['String']['output']>;
  description?: Maybe<Scalars['String']['output']>;
  id: Scalars['String']['output'];
  name: Scalars['String']['output'];
};

export type PublisherFilterInput = {
  and?: InputMaybe<Array<PublisherFilterInput>>;
  contactInformation?: InputMaybe<StringOperationFilterInput>;
  description?: InputMaybe<StringOperationFilterInput>;
  id?: InputMaybe<StringOperationFilterInput>;
  name?: InputMaybe<StringOperationFilterInput>;
  or?: InputMaybe<Array<PublisherFilterInput>>;
};

export type PublisherPatchInput = {
  contactInformation?: InputMaybe<Scalars['String']['input']>;
  description?: InputMaybe<Scalars['String']['input']>;
  name: Scalars['String']['input'];
};

export type PublisherSortInput = {
  contactInformation?: InputMaybe<SortEnumType>;
  description?: InputMaybe<SortEnumType>;
  id?: InputMaybe<SortEnumType>;
  name?: InputMaybe<SortEnumType>;
};

/** A segment of a collection. */
export type PublishersCollectionSegment = {
  __typename?: 'PublishersCollectionSegment';
  /** A flattened list of the items. */
  items?: Maybe<Array<Publisher>>;
  /** Information to aid in pagination. */
  pageInfo: CollectionSegmentInfo;
  totalCount: Scalars['Int']['output'];
};

export type Queries = {
  __typename?: 'Queries';
  author?: Maybe<Author>;
  authors?: Maybe<AuthorsCollectionSegment>;
  member?: Maybe<Member>;
  members?: Maybe<MembersCollectionSegment>;
  publication?: Maybe<Publication>;
  publications?: Maybe<PublicationsCollectionSegment>;
  publisher: Publisher;
  publishers?: Maybe<PublishersCollectionSegment>;
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


export type QueriesMemberArgs = {
  id: Scalars['String']['input'];
};


export type QueriesMembersArgs = {
  order?: InputMaybe<Array<MemberSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<MemberFilterInput>;
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


export type QueriesPublisherArgs = {
  id: Scalars['String']['input'];
};


export type QueriesPublishersArgs = {
  order?: InputMaybe<Array<PublisherSortInput>>;
  skip?: InputMaybe<Scalars['Int']['input']>;
  take?: InputMaybe<Scalars['Int']['input']>;
  where?: InputMaybe<PublisherFilterInput>;
};

export type RenewMembershipError = InvalidFieldError;

export type RenewMembershipInput = {
  id: Scalars['String']['input'];
};

export type RenewMembershipPayload = {
  __typename?: 'RenewMembershipPayload';
  errors?: Maybe<Array<RenewMembershipError>>;
  message?: Maybe<Scalars['String']['output']>;
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

export type SuspendMembershipError = InvalidFieldError;

export type SuspendMembershipInput = {
  id: Scalars['String']['input'];
};

export type SuspendMembershipPayload = {
  __typename?: 'SuspendMembershipPayload';
  errors?: Maybe<Array<SuspendMembershipError>>;
  message?: Maybe<Scalars['String']['output']>;
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

export type UpdateMemberError = InvalidFieldError;

export type UpdateMemberInput = {
  id: Scalars['String']['input'];
  patch: MemberPatchInput;
};

export type UpdateMemberPayload = {
  __typename?: 'UpdateMemberPayload';
  data?: Maybe<Member>;
  errors?: Maybe<Array<UpdateMemberError>>;
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

export type UpdatePublisherError = InvalidFieldError | NotFoundWithTheIdError;

export type UpdatePublisherInput = {
  id: Scalars['String']['input'];
  patch: PublisherPatchInput;
};

export type UpdatePublisherPayload = {
  __typename?: 'UpdatePublisherPayload';
  data?: Maybe<Publisher>;
  errors?: Maybe<Array<UpdatePublisherError>>;
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

export type DeleteAuthorMutationVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type DeleteAuthorMutation = { __typename?: 'Mutations', deleteAuthor: { __typename?: 'DeleteAuthorPayload', message?: string | null, errors?: Array<{ __typename?: 'DeletionFailedError', objectName: string, reason: string, message: string } | { __typename?: 'NotFoundWithTheIdError', id: string, objectName: string, message: string }> | null } };

export type GetAuthorsQueryVariables = Exact<{
  skip: Scalars['Int']['input'];
  take: Scalars['Int']['input'];
  filter?: InputMaybe<AuthorFilterInput>;
  sortBy?: InputMaybe<Array<AuthorSortInput> | AuthorSortInput>;
}>;


export type GetAuthorsQuery = { __typename?: 'Queries', authors?: { __typename?: 'AuthorsCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Author', id: string, firstName: string, lastName: string, nationality: string }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type SearchAuthorsQueryVariables = Exact<{
  filterText: Scalars['String']['input'];
}>;


export type SearchAuthorsQuery = { __typename?: 'Queries', authors?: { __typename?: 'AuthorsCollectionSegment', items?: Array<{ __typename?: 'Author', id: string, fullName: string }> | null } | null };

export type GetAuthorQueryVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type GetAuthorQuery = { __typename?: 'Queries', author?: { __typename?: 'Author', id: string, firstName: string, lastName: string, fullName: string, dateOfBirth: any, dateOfDeath?: any | null, nationality: string, biography: string } | null };

export type CreateMemberMutationVariables = Exact<{
  createMemberInput: CreateMemberInput;
}>;


export type CreateMemberMutation = { __typename?: 'Mutations', createMember: { __typename?: 'CreateMemberPayload', message?: string | null, data?: { __typename?: 'Member', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string }> | null } };

export type UpdateMemberMutationVariables = Exact<{
  id: Scalars['String']['input'];
  memberPatch: MemberPatchInput;
}>;


export type UpdateMemberMutation = { __typename?: 'Mutations', updateMember: { __typename?: 'UpdateMemberPayload', message?: string | null, data?: { __typename?: 'Member', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string }> | null } };

export type GetMembersQueryVariables = Exact<{
  skip: Scalars['Int']['input'];
  take: Scalars['Int']['input'];
  filter?: InputMaybe<MemberFilterInput>;
  sortBy?: InputMaybe<Array<MemberSortInput> | MemberSortInput>;
}>;


export type GetMembersQuery = { __typename?: 'Queries', members?: { __typename?: 'MembersCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Member', id: string, firstName: string, lastName: string, membershipStartDateTime: any, contactNumber: string, email: string, status: MembershipStatus }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type GetMemberQueryVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type GetMemberQuery = { __typename?: 'Queries', member?: { __typename?: 'Member', id: string, fullName: string, firstName: string, lastName: string, status: MembershipStatus, membershipStartDateTime: any, dateOfBirth: any, contactNumber: string, email: string, address: string } | null };

export type AddPublicationMutationVariables = Exact<{
  addPublicationInput: AddPublicationInput;
}>;


export type AddPublicationMutation = { __typename?: 'Mutations', addPublication: { __typename?: 'AddPublicationPayload', message?: string | null, data?: { __typename?: 'Publication', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string }> | null } };

export type UpdatePublicationMutationVariables = Exact<{
  id: Scalars['String']['input'];
  publicationPatch: PublicationPatchInput;
}>;


export type UpdatePublicationMutation = { __typename?: 'Mutations', updatePublication: { __typename?: 'UpdatePublicationPayload', message?: string | null, data?: { __typename?: 'Publication', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string } | { __typename?: 'NotFoundWithTheIdError', id: string, objectName: string, message: string }> | null } };

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


export type GetPublicationQuery = { __typename?: 'Queries', publication?: { __typename?: 'Publication', id: string, title: string, publicationType: PublicationType, isbn?: string | null, edition: string, callNumber: string, language: string, publisher: string, publishedDate: any, buyingPrice: any, copiesAvailable: number, description: string, authors: Array<{ __typename?: 'PublicationAuthor', id: string, firstName: string, lastName: string, fullName: string }> } | null };

export type AddPublisherMutationVariables = Exact<{
  addPublisherInput: AddPublisherInput;
}>;


export type AddPublisherMutation = { __typename?: 'Mutations', addPublisher: { __typename?: 'AddPublisherPayload', message?: string | null, data?: { __typename?: 'Publisher', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string }> | null } };

export type UpdatePublisherMutationVariables = Exact<{
  id: Scalars['String']['input'];
  publisherPatch: PublisherPatchInput;
}>;


export type UpdatePublisherMutation = { __typename?: 'Mutations', updatePublisher: { __typename?: 'UpdatePublisherPayload', message?: string | null, data?: { __typename?: 'Publisher', id: string } | null, errors?: Array<{ __typename?: 'InvalidFieldError', fieldName: string, message: string } | { __typename?: 'NotFoundWithTheIdError', id: string, objectName: string, message: string }> | null } };

export type GetPublishersQueryVariables = Exact<{
  skip: Scalars['Int']['input'];
  take: Scalars['Int']['input'];
  filter?: InputMaybe<PublisherFilterInput>;
  sortBy?: InputMaybe<Array<PublisherSortInput> | PublisherSortInput>;
}>;


export type GetPublishersQuery = { __typename?: 'Queries', publishers?: { __typename?: 'PublishersCollectionSegment', totalCount: number, items?: Array<{ __typename?: 'Publisher', id: string, name: string, description?: string | null, contactInformation?: string | null }> | null, pageInfo: { __typename?: 'CollectionSegmentInfo', hasNextPage: boolean, hasPreviousPage: boolean } } | null };

export type GetPublisherQueryVariables = Exact<{
  id: Scalars['String']['input'];
}>;


export type GetPublisherQuery = { __typename?: 'Queries', publisher: { __typename?: 'Publisher', id: string, name: string, description?: string | null, contactInformation?: string | null } };

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
    override document = AddAuthorDocument;
    
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
    override document = UpdateAuthorDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const DeleteAuthorDocument = gql`
    mutation deleteAuthor($id: String!) {
  deleteAuthor(input: {id: $id}) {
    message
    errors {
      ... on NotFoundWithTheIdError {
        id
        objectName
        message
      }
      ... on DeletionFailedError {
        objectName
        reason
        message
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
  export class DeleteAuthorGQL extends Apollo.Mutation<DeleteAuthorMutation, DeleteAuthorMutationVariables> {
    override document = DeleteAuthorDocument;
    
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
    override document = GetAuthorsDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const SearchAuthorsDocument = gql`
    query searchAuthors($filterText: String!) {
  authors(
    skip: 0
    take: 5
    where: {or: [{firstName: {contains: $filterText}}, {lastName: {contains: $filterText}}]}
    order: {firstName: ASC}
  ) {
    items {
      id
      fullName
    }
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class SearchAuthorsGQL extends Apollo.Query<SearchAuthorsQuery, SearchAuthorsQueryVariables> {
    override document = SearchAuthorsDocument;
    
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
    override document = GetAuthorDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const CreateMemberDocument = gql`
    mutation createMember($createMemberInput: CreateMemberInput!) {
  createMember(input: $createMemberInput) {
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
  export class CreateMemberGQL extends Apollo.Mutation<CreateMemberMutation, CreateMemberMutationVariables> {
    override document = CreateMemberDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const UpdateMemberDocument = gql`
    mutation updateMember($id: String!, $memberPatch: MemberPatchInput!) {
  updateMember(input: {id: $id, patch: $memberPatch}) {
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
  export class UpdateMemberGQL extends Apollo.Mutation<UpdateMemberMutation, UpdateMemberMutationVariables> {
    override document = UpdateMemberDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const GetMembersDocument = gql`
    query getMembers($skip: Int!, $take: Int!, $filter: MemberFilterInput, $sortBy: [MemberSortInput!]) {
  members(skip: $skip, take: $take, where: $filter, order: $sortBy) {
    items {
      id
      firstName
      lastName
      membershipStartDateTime
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
  export class GetMembersGQL extends Apollo.Query<GetMembersQuery, GetMembersQueryVariables> {
    override document = GetMembersDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const GetMemberDocument = gql`
    query getMember($id: String!) {
  member(id: $id) {
    id
    fullName
    firstName
    lastName
    status
    membershipStartDateTime
    dateOfBirth
    contactNumber
    email
    address
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class GetMemberGQL extends Apollo.Query<GetMemberQuery, GetMemberQueryVariables> {
    override document = GetMemberDocument;
    
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
    override document = AddPublicationDocument;
    
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
    override document = UpdatePublicationDocument;
    
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
    override document = GetPublicationsDocument;
    
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
      fullName
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
    override document = GetPublicationDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const AddPublisherDocument = gql`
    mutation addPublisher($addPublisherInput: AddPublisherInput!) {
  addPublisher(input: $addPublisherInput) {
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
  export class AddPublisherGQL extends Apollo.Mutation<AddPublisherMutation, AddPublisherMutationVariables> {
    override document = AddPublisherDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const UpdatePublisherDocument = gql`
    mutation updatePublisher($id: String!, $publisherPatch: PublisherPatchInput!) {
  updatePublisher(input: {id: $id, patch: $publisherPatch}) {
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
  export class UpdatePublisherGQL extends Apollo.Mutation<UpdatePublisherMutation, UpdatePublisherMutationVariables> {
    override document = UpdatePublisherDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const GetPublishersDocument = gql`
    query getPublishers($skip: Int!, $take: Int!, $filter: PublisherFilterInput, $sortBy: [PublisherSortInput!]) {
  publishers(skip: $skip, take: $take, where: $filter, order: $sortBy) {
    items {
      id
      name
      description
      contactInformation
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
  export class GetPublishersGQL extends Apollo.Query<GetPublishersQuery, GetPublishersQueryVariables> {
    override document = GetPublishersDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }
export const GetPublisherDocument = gql`
    query getPublisher($id: String!) {
  publisher(id: $id) {
    id
    name
    description
    contactInformation
  }
}
    `;

  @Injectable({
    providedIn: 'root'
  })
  export class GetPublisherGQL extends Apollo.Query<GetPublisherQuery, GetPublisherQueryVariables> {
    override document = GetPublisherDocument;
    
    constructor(apollo: Apollo.Apollo) {
      super(apollo);
    }
  }