namespace Data.Entities
{
    /// <summary>
    /// Корзина пользователя
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long CartId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public long UserId { get; set; }

        public override string ToString()
        {
            return $"{CartId}, {UserId}";
        }
    }
}
