namespace BookShop.Orders.Manager.Models
{
    public enum OrderStatus
    {
        Complete = 0,
        DeliveryInProgress = 1,
        ReadyToDelivery = 2,
        Assembling = 3,
        Paid = 4,
        UnPaid = 5
    }
}
