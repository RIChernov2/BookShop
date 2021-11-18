using System;
using FluentMigrator;


namespace BookShop.Notifications.Manager.Migrations
{
    [Migration(202110211845)]
    public class AddMessages : Migration
    {
        public static string SchemeName { get; set; }
        private string _typeName = "message_type";
        public override void Up()
        {
            string type = $"\"{SchemeName}\".\"{_typeName}\"";
            Execute.Sql($"CREATE TYPE {type} as ENUM ('Info', 'Warning', 'Ads')");

            Create.Table("messages")
                //.InSchema(SchemeName) //- этого не требуется, така как прописано во флюент миграторе
                .WithColumn("message_id").AsInt64().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt64().NotNullable()
                .WithColumn("date").AsDateTime().NotNullable()
                .WithColumn("type").AsCustom(type).NotNullable()
                .WithColumn("text").AsString(500).NotNullable();

            Create.Index("ix_messages_user_id")
                .OnTable("messages")
                .OnColumn("user_id")
                .Ascending();
        }

        public override void Down()
        {
            Delete.Table("messages");
        }
    }
}
