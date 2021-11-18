
namespace BookShop.BooksAndAuthors.Manager.Configurations.Models
{
    public class RabbitMq
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string BooksRequestQueue { get; set; }
        public string AuthorsRequestQueue { get; set; }
    }
}
