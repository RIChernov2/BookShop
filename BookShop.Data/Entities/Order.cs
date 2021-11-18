using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Entities
{
    /// <summary>
    /// Заказ пользователя
    /// </summary>
    public class Order
    {
        public Order()
        {
            OrderedBooks = new List<OrderedBook>();
        }

        public Order(long orderId, long userId, long addressId, List<OrderedBook> orderedBooks, byte orderStatus,
            DateTime creationDate)
        {
            OrderId = orderId;
            UserId = userId;
            AddressId = addressId;
            OrderedBooks = orderedBooks;
            OrderStatus = orderStatus;
            CreationDate = creationDate;
        }

        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long AddressId { get; set; }
        public List<OrderedBook> OrderedBooks { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime CreationDate { get; set; }

        public override string ToString()
        {
            return $"{OrderId}, {UserId}, {AddressId}, {string.Join(", ", OrderedBooks.Select(ob => ob.OrderedBookId))}, {OrderStatus}, {CreationDate}";
        }
    }

    public class NewOrderInfo
    {
        public long UserId { get; set; }
        public long AddressId { get; set; }
        public long CartId { get; set; }
        public List<CartProductExtended> CartProducts { get; set; }

        public override string ToString()
        {
            return $"{UserId}, {AddressId}, {CartId}, {string.Join(", ", CartProducts.Select(cp => cp.CartProductId))}";
        }
    }
}
