import { Author } from "./author.model";

export class Book {
  bookId: number = -1;
  title: string = '';
  description: string  = '';
  author: Author | null = null;
  price: number = -1;
}