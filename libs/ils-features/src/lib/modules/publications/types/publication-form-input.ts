import { PublicationType } from '@kathanika/graphql-ts-client';
import { PublicationAuthor } from './publication-author';

export type PublicationFormInput = {
  title: string;
  publicationType: PublicationType;
  publishedDate: Date;
  publisher: string;
  isbn?: string | null;
  edition: string;
  language: string;
  description: string;
  authors: PublicationAuthor[];
  callNumber: string;
  copiesAvailable: number;
};
