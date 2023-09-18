import { PublicationType } from "src/app/graphql/generated/graphql-operations";

export type PublicationFormInput = {
  title: string;
  publicationType: PublicationType;
  publishedDate: Date;
  publisher: string;
  isbn?: string | null;
  edition: string;
  language: string;
  description: string;
  authors: string[];
  buyingPrice: number;
  callNumber: string;
  copiesAvailable: number;
}
