using System;
using System.Data;
using BookShop.Orders.Manager.Configurations.Models;
using BookShop.Orders.Manager.Managers.Interfaces;
using BookShop.Orders.Manager.Models;
using BookShop.Orders.Manager.Repositories;
using BookShop.Orders.Manager.Repositories.Interfaces;
using Npgsql;

namespace BookShop.Orders.Manager.Managers
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IOrdersRepository _ordersRepository;
        private IOrderedBooksRepository _orderedBooksRepository;
        private AppConfiguration _configuration;

        public UnitOfWork(AppConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(configuration.ConnectionStrings.DefaultConnection);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IOrdersRepository OrdersRepository
            => _ordersRepository ??= new OrdersDatabaseRepository(_transaction, _configuration);
        public IOrderedBooksRepository OrderedBooksRepository
            => _orderedBooksRepository ??= new OrderedBooksDatabaseRepository(_transaction, _configuration);

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        private void ResetRepositories()
        {
            _ordersRepository = null;
            _orderedBooksRepository = null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                ReleaseUnmanagedResources();
            }
        }

        private void ReleaseUnmanagedResources()
        {
            _transaction?.Dispose();
            _transaction = null;
            _connection?.Dispose();
            _connection = null;
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}