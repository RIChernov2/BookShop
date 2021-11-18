using System.Data;
using FluentMigrator;

namespace Data.Migrations
{
    [Migration(202107280129)]
    public class AddAddresses : Migration
    {
        public override void Up()
        {
            Create.Table("addresses")
                .WithColumn("address_id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("user_id").AsInt64().NotNullable()
                .WithColumn("address_name").AsString(50).NotNullable()
                .WithColumn("country").AsString(50).Nullable()
                .WithColumn("district").AsString(50).Nullable()
                .WithColumn("city").AsString(30).NotNullable()
                .WithColumn("street").AsString(30).NotNullable()
                .WithColumn("home").AsString(15).NotNullable()
                .WithColumn("apartments").AsString(15).Nullable();

            Create.ForeignKey("fk_addresses_users_user_id")
                .FromTable("addresses").ForeignColumn("user_id")
                .ToTable("users").PrimaryColumn("user_id")
                .OnDeleteOrUpdate(Rule.Cascade);

            Create.Index("ix_addresses_user_id")
                .OnTable("addresses")
                .OnColumn("user_id")
                .Ascending();

            Create.Index("ix_addresses_city")
                .OnTable("addresses")
                .OnColumn("city")
                .Ascending();

            Create.Index("ix_addresses_street")
                .OnTable("addresses")
                .OnColumn("street")
                .Ascending();
        }

        public override void Down()
        {
            Delete.Table("addresses");
        }
    }
}
