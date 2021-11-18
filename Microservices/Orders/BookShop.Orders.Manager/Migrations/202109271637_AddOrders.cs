using System.Data;
using FluentMigrator;

namespace Data.Migrations
{
    [Migration(202109271657)]
    public class AddOrders : Migration
    {
        public override void Up()
        {
            Create.Table("orders")
                .WithColumn("order_id").AsInt64().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt64().NotNullable()
                .WithColumn("address_id").AsInt64().NotNullable()
                .WithColumn("creation_date").AsDateTime().NotNullable()
                .WithColumn("order_status").AsByte().NotNullable();
  
            Create.Index("ix_orders_user_id")
                .OnTable("orders")
                .OnColumn("user_id")
                .Ascending();

            Create.Index("ix_orders_creation_date")
                .OnTable("orders")
                .OnColumn("creation_date")
                .Ascending();

            Create.Index("ix_orders_order status")
                .OnTable("orders")
                .OnColumn("order_status")
                .Ascending();
        }

        public override void Down()
        {
            Delete.Table("orders");
        }
    }
}
