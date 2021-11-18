using BookShop.Notifications.Manager.Factories.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace BookShop.Notifications.Manager.Factories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<DatabaseConnectionTypes, string> _connectionDictionary;

        public DbConnectionFactory(IDictionary<DatabaseConnectionTypes, string> connectionDictionary)
        {
            _connectionDictionary = connectionDictionary;
        }

        public IDbConnection CreateDbConnection(DatabaseConnectionTypes connectionType)
            => _connectionDictionary.TryGetValue(connectionType, out var connectionString)
                ? new NpgsqlConnection(connectionString)
                : throw new ArgumentException(nameof(connectionType));
    }
}
