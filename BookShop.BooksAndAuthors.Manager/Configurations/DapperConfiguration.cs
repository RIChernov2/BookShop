using Microsoft.Extensions.DependencyInjection;

namespace BookShop.BooksAndAuthors.Manager.Configurations
{
    public static class DapperConfiguration
    {
        /// <summary>
        /// Исправляет меппинг CamelCase в snake_case
        /// иначе при обращении к базе даппер не сопоставляет (например) user_id из БД
        /// и поля UserId класса, поле получает значение по умолчанию
        /// </summary>
        public static void AddSnakeCaseMapping()
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
        }
    }
}
