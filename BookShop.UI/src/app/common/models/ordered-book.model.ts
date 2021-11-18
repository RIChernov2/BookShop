import { Book } from "./book.model";

export class OrderedBook {
  orderedBookId: number = -1;
  orderId: number = -1;
  bookId: number = -1;
  retailPrice: number = -1;
  amount: number = -1;
}

export class OrderedBookExtended {
  orderedBookId!: number;
  orderId!: number;
  book: Book;
  retailPrice!: number;
  amount!: number;

  constructor(orderedBook: OrderedBook, book: Book) {
    const { bookId, ...props } = orderedBook;
    Object.assign(this, props);
    this.book = book;
  }
}