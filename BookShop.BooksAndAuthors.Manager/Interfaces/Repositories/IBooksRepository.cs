using BookShop.BooksAndAuthors.Manager.Data.Entities;

namespace BookShop.BooksAndAuthors.Manager.Interfaces.Repositories
{
    public interface IBooksRepository : IGenericRepository<Book, long>
    {
    }
}