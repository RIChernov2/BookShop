using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Data.Factories.Interfaces;
using Npgsql;

namespace Data.Factories
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