using FluentMigrator;
using System;

namespace Data.Migrations
{
    [Profile("FirstStart")]
    public class InitialDataMigration : Migration {
        public override void Up()
        {
            Insert.IntoTable("orders")
                .Row(new { user_id = 1, address_id = 1, order_status = 0, creation_date = DateTime.Now.AddDays(-18) })
                .Row(new { user_id = 2, address_id = 2, order_status = 1, creation_date = DateTime.Now.AddDays(-10).AddHours(11) })
                .Row(new { user_id = 4, address_id = 4, order_status = 3, creation_date = DateTime.Now.AddDays(-5).AddHours(-2) })
                .Row(new { user_id = 5, address_id = 6, order_status = 2, creation_date = DateTime.Now });

            Insert.IntoTable("ordered_books")
                .Row(new { order_id = 1, book_id = 1, retail_price = 100, amount = 3 })
                .Row(new { order_id = 1, book_id = 3, retail_price = 300, amount = 1 })
                .Row(new { order_id = 1, book_id = 4, retail_price = 400, amount = 1 })
                .Row(new { order_id = 2, book_id = 5, retail_price = 500, amount = 1 })
                .Row(new { order_id = 2, book_id = 2, retail_price = 200, amount = 1 })
                .Row(new { order_id = 2, book_id = 3, retail_price = 300, amount = 1 })
                .Row(new { order_id = 3, book_id = 7, retail_price = 700, amount = 1 })
                .Row(new { order_id = 3, book_id = 6, retail_price = 600, amount = 1 })
                .Row(new { order_id = 4, book_id = 3, retail_price = 300, amount = 1 })
                .Row(new { order_id = 4, book_id = 4, retail_price = 400, amount = 2 })
                .Row(new { order_id = 4, book_id = 1, retail_price = 100, amount = 2 });
        }

        public override void Down()
        {
        }
    }
}
