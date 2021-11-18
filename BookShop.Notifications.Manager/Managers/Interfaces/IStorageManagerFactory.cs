namespace BookShop.Notifications.Manager.Managers.Interfaces
{
    public interface IStorageManagerFactory<out T>
    {
        public T Create();
    }
}