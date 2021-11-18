using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using BookShop.BooksAndAuthors.Manager.Providers;
using BookShop.BooksAndAuthors.Manager.StorageManagers;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.BooksAndAuthors.Manager.Configurations
{
    public class RabbitMqConfiguration
    {
        public static void Configure(AppConfiguration configuration)
        {
            var rabbitMqSettings = configuration.RabbitMq;

            var connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqSettings.Host,
                UserName = rabbitMqSettings.Username,
                Password = rabbitMqSettings.Password,
                DispatchConsumersAsync = true
            };

            var connection = connectionFactory.CreateConnection();

            var bookFactory = new BooksStorageManagerFactory(configuration);
            var bookConsumer = new BookProvider(connection, rabbitMqSettings.BooksRequestQueue, bookFactory);
            bookConsumer.SetDeliverySettings();

            var authorFactory = new AuthorsStorageManagerFactory(configuration);
            var authorConsumer = new AuthorProvider(connection, rabbitMqSettings.AuthorsRequestQueue, authorFactory);
            authorConsumer.SetDeliverySettings();
        }
    }
}
