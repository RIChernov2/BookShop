using System;

namespace Data.Entities
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

        public OrderedBook()
        {
        }

        public OrderedBook(long orderedBookId, long orderId, long bookId, decimal retailPrice, int amount)
        {
            OrderedBookId = orderedBookId;
            OrderId = orderId;
            BookId = bookId;
            RetailPrice = retailPrice;
            Amount = amount;
        }

        public OrderedBook(Book book, int amount) : this(-1, -1, book.BookId, book.Price, amount)
        {
            if (amount < 1) throw new ArgumentException("Amount can't be less than 1", nameof(amount));
        }

        public override string ToString()
        {
            return $"{OrderedBookId}, {OrderId}, {BookId}, {RetailPrice}, {Amount}";
        }
    }
}
