using BookShop.Orders.Manager.Configurations.Models;
using BookShop.Orders.Manager.Managers.Interfaces;

namespace BookShop.Orders.Manager.Managers.Factories
{
    public class OrderedBooksStorageManagerFactory : IStorageManagerFactory<IOrderedBooksStorageManager>
    {
        private readonly AppConfiguration _configuration;

        public OrderedBooksStorageManagerFactory(AppConfiguration configuration)
        {
            _configuration = configuration;
        }
    
        public IOrderedBooksStorageManager Create()
        {
            return new OrderedBooksStorageManager(new UnitOfWork(_configuration));
        }
    }
}