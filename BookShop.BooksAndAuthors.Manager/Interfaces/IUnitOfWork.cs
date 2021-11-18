using BookShop.BooksAndAuthors.Manager.Interfaces.Repositories;
using System;

namespace BookShop.BooksAndAuthors.Manager.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IAuthorsRepository AuthorsRepository { get; }
        IBooksRepository BooksRepository { get; }
    }
}
