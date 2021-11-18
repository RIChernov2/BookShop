namespace BookShop.Orders.Manager.Models
{
    public class OrderedBook
    {
        public long OrderedBookId { get; set; }

        /// <summary>
        /// Id заказа
        /// </summary>
        public long OrderId { get; set; }

        /// <summary>
        /// Id купленной книги
        /// </summary>
        public long BookId { get; set; }

        /// <summary>
        /// Цена одной книги на момент покупки
        /// </summary>
        public decimal RetailPrice { get; set; }

        /// <summary>
        /// Количество купленных книг
        /// </summary>
        public int Amount { get; set; }
    }
}
