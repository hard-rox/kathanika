import { PublicationType } from "src/app/graphql/generated/graphql-operations";

export type PublicationFormOutput = {
  title: string;
  publicationType: PublicationType;
  publishedDate: Date | null;
  publisher: string;
  isbn: string | null;
  edition: string;
  language: string;
  description: string | null;
  authorIds: string[] | null;
  buyingPrice: number;
  callNumber: string;
  copiesPurchased: number;
}
