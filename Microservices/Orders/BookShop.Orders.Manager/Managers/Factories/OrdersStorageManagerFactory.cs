using BookShop.Orders.Manager.Configurations.Models;
using BookShop.Orders.Manager.Managers.Interfaces;

namespace BookShop.Orders.Manager.Managers.Factories
{
    public class OrdersStorageManagerFactory : IStorageManagerFactory<IOrdersStorageManager>
    {
        private readonly AppConfiguration _configuration;

        public OrdersStorageManagerFactory(AppConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IOrdersStorageManager Create()
        {
            return new OrdersStorageManager(new UnitOfWork(_configuration));
        }
    }
}