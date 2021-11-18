using BookShop.BooksAndAuthors.Manager.Configurations.Models;
using BookShop.BooksAndAuthors.Manager.Interfaces.Managers;
using BookShop.BooksAndAuthors.Manager.Repositories;

namespace BookShop.BooksAndAuthors.Manager.StorageManagers
{
    public class AuthorsStorageManagerFactory : IStorageManagerFactory<IAuthorsStorageManager>
    {
        private readonly AppConfiguration _configuration;

        public AuthorsStorageManagerFactory(AppConfiguration configuration)
        {
            _configuration = configuration;
        }
         
        public IAuthorsStorageManager Create()
        {
            return new AuthorsStorageManager(new UnitOfWork(_configuration), _configuration);
        }
    }
}
