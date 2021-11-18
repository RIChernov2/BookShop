using BookShop.Orders.Manager.Configurations.Models;

namespace BookShop.Configuration.Models
{
    public class AppConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public string FluentMigratorProfile { get; set; }
        public RabbitMq RabbitMq { get; set; }
        public Authentication Authentication { get; set; }
        public string SchemaName { get; set; }
    }
}