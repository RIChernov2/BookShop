using System;
using FluentMigrator;


namespace BookShop.Notifications.Manager.Migrations
{
    [Migration(202110270530)]
    public class AddNotificationSettings : Migration
    {
        //public static string SchemeName { get; set; }
        public override void Up()
        {

            Create.Table("notification_settings")
                //.InSchema(SchemeName) //- этого не требуется, така как прописано во флюент миграторе
                .WithColumn("notification_setting_id").AsInt64().PrimaryKey().Identity()
                .WithColumn("user_id").AsInt64().NotNullable().Unique()
                .WithColumn("email_settings").AsString(50).NotNullable()
                .WithColumn("push_settings").AsString(50).NotNullable()
                .WithColumn("email").AsString(50).Nullable();

            Create.Index("ix_notification_settings_user_id")
                .OnTable("notification_settings")
                .OnColumn("user_id")
                .Ascending();

        }

        public override void Down()
        {
            Delete.Table("notification_settings");
        }
    }
}
