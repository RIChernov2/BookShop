using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using BookShop.BooksAndAuthors.Manager.Interfaces.Managers;
using BookShop.BooksAndAuthors.Manager.Repositories;

namespace BookShop.BooksAndAuthors.Manager.StorageManagers
{
    public class BooksStorageManagerFactory : IStorageManagerFactory<IBooksStorageManager>
    {
        private readonly AppConfiguration _configuration;

        public BooksStorageManagerFactory(AppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IBooksStorageManager Create()
        {
            return new BooksStorageManager(new UnitOfWork(_configuration), _configuration);
        }
    }
}
