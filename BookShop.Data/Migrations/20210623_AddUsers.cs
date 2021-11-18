using FluentMigrator;

namespace Data.Migrations
{
    [Migration(202106231927)]
    public class AddUsers : Migration
    {
        public override void Up()
        {
            Create.Table("users")
                .WithColumn("user_id").AsInt64().PrimaryKey().Identity().NotNullable()
                .WithColumn("name").AsString(50).NotNullable()
                .WithColumn("surname").AsString(50).Nullable()
                .WithColumn("age").AsByte().Nullable()
                .WithColumn("email").AsString(50).NotNullable()
                .WithColumn("password").AsString(50).NotNullable();

            Create.Index("ix_users_surname")
                .OnTable("users")
                .OnColumn("surname");

            Create.Index("ix_users_email")
                .OnTable("users")
                .OnColumn("email");
        }

        public override void Down()
        {
            Delete.Table("users");
        }
    }
}