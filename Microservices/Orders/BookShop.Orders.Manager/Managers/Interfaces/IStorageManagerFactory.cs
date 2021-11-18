namespace BookShop.Orders.Manager.Managers.Interfaces
{
    public interface IStorageManagerFactory<out T>
    {
        public T Create();
    }
}