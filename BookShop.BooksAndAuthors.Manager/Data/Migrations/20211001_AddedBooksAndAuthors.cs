using System.Data;
using FluentMigrator;

namespace BookShop.BooksAndAuthors.Manager.Data.Migrations
{
    //Формат версии год месяц день часы минуты
    [Migration(202110011400)]
    public class AddedBooksAndAuthors : Migration
    {
        public override void Up()
        {
            Create.Table("authors")
                .WithColumn("author_id").AsInt64().PrimaryKey().Unique().Identity()
                .WithColumn("name").AsString(30).NotNullable()
                .WithColumn("surname").AsString(30).NotNullable()
                .WithColumn("age").AsInt16().Nullable();

            Create.Table("books")
                .WithColumn("book_id").AsInt64().PrimaryKey().Identity()
                .WithColumn("author_id").AsInt64().NotNullable()
                .WithColumn("title").AsString(150).NotNullable()
                .WithColumn("description").AsString().Nullable()
                .WithColumn("price").AsDecimal().NotNullable();

            Create.Index("ix_books_author_id")
                .OnTable("books")
                .OnColumn("author_id")
                .Ascending();
            
            Create.Index("ix_books_title")
                .OnTable("books")
                .OnColumn("title")
                .Ascending();
            
            Create.ForeignKey("fk_books_authors_author_id")
                .FromTable("books").ForeignColumn("author_id")
                .ToTable("authors").PrimaryColumn("author_id")
                .OnDeleteOrUpdate(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("authors");
            Delete.Table("books"); 
        }
    }
}