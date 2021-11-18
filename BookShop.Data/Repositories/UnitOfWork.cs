using System;
using System.Data;
using Data.Factories.Interfaces;
using Data.Repositories.DbRepositories;
using Data.Repositories.Interfaces;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private IAddressesRepository _adressRepository;
        private ICartProductsRepository _cartProductsRepository;
        private ICartsRepository _cartsRepository;
        private IUsersRepository _usersRepository;

        public UnitOfWork(IDbConnectionFactory dbConnectionFactory,
            DatabaseConnectionTypes type = DatabaseConnectionTypes.Default
        )
        {
            _connection = dbConnectionFactory.CreateDbConnection(type);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IAddressesRepository AdressesRepository
            => _adressRepository ??= new AddressesDatabaseRepository(_transaction);

        public ICartsRepository CartsRepository
            => _cartsRepository ??= new CartsDatabaseRepository(_transaction);

        public ICartProductsRepository CartProductsRepository
            => _cartProductsRepository ??= new CartProductsDatabaseRepository(_transaction);

        public IUsersRepository UsersRepository
            => _usersRepository ??= new UsersDatabaseRepository(_transaction);


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
            _adressRepository = null;
            _cartsRepository = null;
            _cartProductsRepository = null;
            _usersRepository = null;
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