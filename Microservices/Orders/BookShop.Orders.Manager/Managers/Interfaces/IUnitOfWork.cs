using System;
using BookShop.Orders.Manager.Repositories.Interfaces;

namespace BookShop.Orders.Manager.Managers.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IOrdersRepository OrdersRepository { get; }
        IOrderedBooksRepository OrderedBooksRepository { get; }
        void Commit();
    }
}