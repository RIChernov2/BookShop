namespace BookShop.Configuration.Models
{
    public class Authentication
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
    }
}