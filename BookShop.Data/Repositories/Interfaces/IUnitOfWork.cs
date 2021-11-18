using System;

namespace Data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressesRepository AdressesRepository { get; }
        ICartProductsRepository CartProductsRepository { get; }
        ICartsRepository CartsRepository { get; }
        IUsersRepository UsersRepository { get; }
        void Commit();
    }
}