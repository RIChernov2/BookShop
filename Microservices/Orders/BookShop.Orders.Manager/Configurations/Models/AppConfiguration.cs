using NLog.Config;

namespace BookShop.Orders.Manager.Configurations.Models
{
    public class AppConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string FluentMigratorProfile { get; set; }
        public RabbitMq RabbitMq { get; set; }
        public LoggingConfiguration NLogConfiguration { get; set; }
        public string SchemaName { get; set; }
    }
}