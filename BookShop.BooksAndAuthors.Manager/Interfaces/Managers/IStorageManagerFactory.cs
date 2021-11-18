
namespace BookShop.BooksAndAuthors.Manager.Interfaces.Managers
{
    public interface IStorageManagerFactory<out T>
    {
        public T Create();
    }
}
