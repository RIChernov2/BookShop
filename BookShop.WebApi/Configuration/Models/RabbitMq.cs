namespace BookShop.Configuration.Models
{
    public class RabbitMq
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string OrdersRequestQueue { get; set; }
        public string OrderedBooksRequestQueue { get; set; }
        public string MessagesRequestQueue { get; set; }
        public string NSettingsRequestQueue { get; set; }
        public string PushRequestQueue { get; set; }
        public string BooksRequestQueue { get; set; }
        public string AuthorsRequestQueue { get; set; }
    }
}