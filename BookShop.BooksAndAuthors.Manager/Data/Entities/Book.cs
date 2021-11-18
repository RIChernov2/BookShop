namespace BookShop.BooksAndAuthors.Manager.Data.Entities
{
    /// <summary>
    /// Книга
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Id книги
        /// </summary>
        public long BookId { get; set; }

        /// <summary>
        /// Автор книги (для простоты: у книги может быть только 1 автор)
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание книги
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Цена книги
        /// </summary>
        public decimal Price { get; set; }
    }
}
