import { PublicationType } from "src/app/graphql/generated/graphql-operations";

export type PublicationFormOutput = {
  title: string;
  publicationType: PublicationType;
  publishedDate: Date;
  publisher: string;
  isbn?: string;
  edition: string;
  language: string;
  description: string;
  authors: [];
  buyingPrice: number;
  callNumber: string;
  copiesAvailable: number;
}
