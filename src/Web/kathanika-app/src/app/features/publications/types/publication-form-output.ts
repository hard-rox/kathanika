import { PublicationType } from "src/app/graphql/generated/graphql-operations";

export type PublicationFormOutput = {
  title: string;
  publicationType: PublicationType;
  publishedDate: Date;
  publisher: string;
  isbn: string;
  // edition: string;
  // language: string;
  // description: string;
  authorIds: [];
  buyingPrice: number;
  callNumber: string;
  copiesPurchased: number;
}
