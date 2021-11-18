using System;
using System.Data;
using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using BookShop.BooksAndAuthors.Manager.Interfaces;
using BookShop.BooksAndAuthors.Manager.Interfaces.Repositories;
using Npgsql;

namespace BookShop.BooksAndAuthors.Manager.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IAuthorsRepository _authorsRepository;
        private IBooksRepository _booksRepository;
        private AppConfiguration _configuration;

        public UnitOfWork(AppConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new NpgsqlConnection(configuration.ConnectionStrings.DefaultConnection);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IAuthorsRepository AuthorsRepository
            => _authorsRepository ??= new AuthorsDatabaseRepository(_transaction, _configuration);

        public IBooksRepository BooksRepository
            => _booksRepository ??= new BooksDatabaseRepository(_transaction, _configuration);

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
            _authorsRepository = null;
            _booksRepository = null;
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