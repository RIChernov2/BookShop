using System.Data;
using FluentMigrator;

namespace Data.Migrations
{
    [Migration(202108262121)]
    public class AddCartsAndCartProducts : Migration
    {
        public override void Up()
        {
            Create.Table("carts")
                .WithColumn("cart_id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("user_id").AsInt64().NotNullable().Unique();

            Create.Table("cart_products")
                .WithColumn("cart_product_id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("cart_id").AsInt64().NotNullable()
                .WithColumn("book_id").AsInt64().NotNullable()
                .WithColumn("amount").AsInt64().NotNullable();

            Create.Index("ix_carts_user_id")
                .OnTable("carts")
                .OnColumn("user_id")
                .Ascending();

            Create.Index("ix_cart_products_cart_id")
                .OnTable("cart_products")
                .OnColumn("cart_id");

            Create.Index("ix_cart_products_book_id")
                .OnTable("cart_products")
                .OnColumn("book_id");

            Create.ForeignKey("fk_carts_users_user_id")
                .FromTable("carts").ForeignColumn("user_id")
                .ToTable("users").PrimaryColumn("user_id")
                .OnDeleteOrUpdate(Rule.Cascade);

            Create.ForeignKey("fk_cart_products_carts_cart_id")
                .FromTable("cart_products").ForeignColumn("cart_id")
                .ToTable("carts").PrimaryColumn("cart_id")
                .OnDeleteOrUpdate(Rule.Cascade);

            //Create.ForeignKey("fk_cart_products_books_book_id")
            //    .FromTable("cart_products").ForeignColumn("book_id")
            //    .ToTable("books").PrimaryColumn("book_id")
            //    .OnDeleteOrUpdate(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.Table("carts");
            Delete.Table("cart_products");
        }
    }
}
