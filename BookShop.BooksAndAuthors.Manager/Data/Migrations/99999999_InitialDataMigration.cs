using FluentMigrator;

namespace BookShop.BooksAndAuthors.Manager.Data.Migrations
{
    [Profile("FirstStart")]
    public class InitialDataMigration : Migration
    {
        public override void Up()
        {
            Insert.IntoTable("authors")
                .Row(new { name = "Федор", surname = "Михайлович Достоевский", age = 22 })
                .Row(new { name = "Лев", surname = "Николаевич Толстой", age = 43 })
                .Row(new { name = "Александр", surname = "Сергеевич Пушкин", age = 23 })
                .Row(new { name = "Николай", surname = "Васильевич Гоголь", age = 44 })
                .Row(new { name = "Лермонтов", surname = "Михаил Юрьевич", age = 35 });

            Insert.IntoTable("books")
                .Row(new { title = "Герой нашего времени", description = "Desc 1", author_id = 5, price = 1500 })
                .Row(new { title = "Мёртвые души", description = "Desc 2", author_id = 4, price = 1200 })
                .Row(new { title = "Мцыри", description = "Desc 3", author_id = 5, price = 900 })
                .Row(new { title = "Исповедь", description = "Desc 4", author_id = 2, price = 1000 })
                .Row(new { title = "Война и мир", description = "Desc 5", author_id = 2, price = 2600 })
                .Row(new { title = "Преступление и наказание", description = "Desc 6", author_id = 1, price = 1200 })
                .Row(new { title = "Капитанская дочка", description = "Desc 7", author_id = 3, price = 1100 })
                .Row(new { title = "Дубровский", description = "Desc 7", author_id = 3, price = 1100 })
                .Row(new { title = "Руслан и Людмила", description = "Desc 7", author_id = 3, price = 1100 });







        }

        public override void Down()
        {
        }
    }
}
