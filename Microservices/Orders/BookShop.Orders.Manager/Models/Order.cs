using System;
using System.Collections.Generic;

namespace BookShop.Orders.Manager.Models
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

        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long AddressId { get; set; }
        public List<OrderedBook> OrderedBooks { get; set; }
        public byte OrderStatus { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
