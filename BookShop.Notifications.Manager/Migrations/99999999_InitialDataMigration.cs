using BookShop.Notifications.Manager.Models;
using FluentMigrator;
using System;

namespace BookShop.Notifications.Manager.Migrations
{
    [Profile("FirstStart")]
    public class InitialDataMigration : Migration
    {
        public override void Up()
        {
            var typeInfo = Enum.GetName(typeof(MessageType), MessageType.Info);
            var typeWarning = Enum.GetName(typeof(MessageType), MessageType.Warning);
            var typeAds = Enum.GetName(typeof(MessageType), MessageType.Ads);

            Insert.IntoTable("messages")
            .Row(new { user_id = "1", date = new DateTime(2021, 09, 01), type = typeInfo, text = "Ваш заказ сформирован." })
            .Row(new { user_id = "5", date = new DateTime(2021, 09, 05), type = typeWarning, text = "Срок действия подарочной карты истекает." })
            .Row(new { user_id = "2", date = new DateTime(2021, 09, 11), type = typeAds, text = "Специальное предложение для вас." })
            .Row(new { user_id = "1", date = new DateTime(2021, 09, 17), type = typeInfo, text = "Ваш заказ направлен курьеру." })
            .Row(new { user_id = "1", date = new DateTime(2021, 09, 21), type = typeWarning, text = "Срок действия подарочной карты истекает." })
            .Row(new { user_id = "6", date = new DateTime(2021, 09, 24), type = typeInfo, text = "Ваш заказ сформирован." })
            .Row(new { user_id = "3", date = new DateTime(2021, 10, 01), type = typeAds, text = "Специальное предложение для вас." })
            .Row(new { user_id = "6", date = new DateTime(2021, 10, 07), type = typeInfo, text = "Ваш заказ направлен курьеру." })
            .Row(new { user_id = "4", date = new DateTime(2021, 10, 12), type = typeAds, text = "Специальное предложение для вас." })
            .Row(new { user_id = "4", date = new DateTime(2021, 10, 19), type = typeInfo, text = "Ваш заказ сформирован." })
            .Row(new { user_id = "4", date = new DateTime(2021, 10, 22), type = typeInfo, text = "Ваш заказ направлен курьеру." })
            .Row(new { user_id = "1", date = new DateTime(2021, 10, 29), type = typeWarning, text = "Срок действия подарочной карты истекает." });

            Insert.IntoTable("notification_settings")
            .Row(new { user_id = "1", email_settings = "{\"Info\":true,\"Warning\":false,\"Ads\":false}", push_settings = "{\"Info\":false,\"Warning\":false,\"Ads\":true}" })
            .Row(new { user_id = "2", email_settings = "{\"Info\":false,\"Warning\":false,\"Ads\":false}", push_settings = "{\"Info\":false,\"Warning\":false,\"Ads\":false}" })
            .Row(new { user_id = "3", email_settings = "{\"Info\":false,\"Warning\":false,\"Ads\":true}", push_settings = "{\"Info\":true,\"Warning\":false,\"Ads\":false}" })
            .Row(new { user_id = "4", email_settings = "{\"Info\":true,\"Warning\":true,\"Ads\":true}", push_settings = "{\"Info\":true,\"Warning\":true,\"Ads\":true}" })
            .Row(new { user_id = "5", email_settings = "{\"Info\":false,\"Warning\":true,\"Ads\":false}", push_settings = "{\"Info\":false,\"Warning\":true,\"Ads\":false}" })
            .Row(new { user_id = "6", email_settings = "{\"Info\":false,\"Warning\":false,\"Ads\":false}", push_settings = "{\"Info\":false,\"Warning\":false,\"Ads\":false}" });


        }

        public override void Down()
        {
        }
    }
}
