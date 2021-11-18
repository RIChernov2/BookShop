import { Address } from "./address.model";
import { CartProductExtended } from "./cart-product.model";
import { OrderStatus } from "./order-status.model";
import { OrderedBook, OrderedBookExtended } from "./ordered-book.model";

export class Order {
  orderId: number = -1;
  userId: number = -1;
  addressId: number = -1;
  orderedBooks: OrderedBook[] = [];
  orderStatus: OrderStatus = OrderStatus.None;
  creationDate: string = '';
}

export class OrderExtended {
  orderId!: number;
  userId!: number;
  address: Address;
  orderedBooks: OrderedBookExtended[];
  orderStatus!: OrderStatus;
  creationDate!: string;

  constructor(order: Order, address: Address, orderedBooksExtended: OrderedBookExtended[]) {
    const { addressId, orderedBooks, ...props } = order;
    Object.assign(this, props);
    this.address = address;
    this.orderedBooks = orderedBooksExtended;
  }
}

export class NewOrderInfo {
  userId: number = -1;
  addressId: number = -1;
  cartId: number = -1;
  cartProducts: CartProductExtended[] = [];

  constructor(init?: Partial<NewOrderInfo>) {
    Object.assign(this, init);
  }
}