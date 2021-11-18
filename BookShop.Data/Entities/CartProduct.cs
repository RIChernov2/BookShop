namespace Data.Entities
{
    public class CartProductBase
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long CartProductId { get; set; }

        /// <summary>
        /// Id корзины
        /// </summary>
        public long CartId { get; set; }

        /// <summary>
        /// Кол-во товара, выбранное пользователем
        /// </summary>
        public int Amount { get; set; }
    }

    /// <summary>
    /// Товар корзины
    /// </summary>
    public class CartProduct : CartProductBase
    {
        /// <summary>
        /// Id книги
        /// </summary>
        public long BookId { get; set; }

        public override string ToString()
        {
            return $"{CartProductId}, {CartId}, {BookId}, {Amount}";
        }
    }

    public class CartProductExtended : CartProductBase
    {
        public Book Book { get; set; }
    }
}
