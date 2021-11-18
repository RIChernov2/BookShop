using System.Collections.Generic;

namespace Data.Entities
{
    /// <summary>
    /// Автор
    /// </summary>
    public class Author
    {
        /// <summary>
        /// Id автора
        /// </summary>
        public long AuthorId { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public byte? Age { get; set; }
    }
}
