using FluentMigrator;
using System.Data;

namespace Data.Migrations
{
    [Migration(202109271700)]
    public class AddOrderedBooks : Migration
    {
        public override void Up()
        {
            Create.Table("ordered_books")
                .WithColumn("ordered_book_id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("order_id").AsInt64().NotNullable()
                .WithColumn("book_id").AsInt64().NotNullable()
                .WithColumn("retail_price").AsDecimal().NotNullable()
                .WithColumn("amount").AsInt32().NotNullable();
            
            Create.ForeignKey("fk_ordered_books_orders_order_id")
                .FromTable("ordered_books").ForeignColumn("order_id")
                .ToTable("orders").PrimaryColumn("order_id")
                .OnDeleteOrUpdate(Rule.Cascade);
            
            Create.Index("ix_ordered_books_order_id")
                .OnTable("ordered_books")
                .OnColumn("order_id")
                .Ascending();
        }

        public override void Down()
        {
            Delete.Table("ordered_books");
        }
    }
}
