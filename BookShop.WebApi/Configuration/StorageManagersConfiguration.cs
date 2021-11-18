using Core.StorageManagers;
using Core.StorageManagers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Configuration
{
    public static class StorageManagersConfiguration
    {
        /// <summary>
        /// ������������ ��� Scoped ��� StorageManager, ������������ � �������
        /// </summary>
        /// <param name="services"></param>
        public static void AddStorageManagers(this IServiceCollection services)
        {
            services.AddScoped<IAddressesStorageManager, AddressesStorageManager>();
            services.AddScoped<ICartProductsStorageManager, CartProductsStorageManager>();
            services.AddScoped<ICartsStorageManager, CartsStorageManager>();
            services.AddScoped<IUsersStorageManager, UsersStorageManager>();
        }
    }
}