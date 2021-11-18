namespace Data.Entities
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public override string ToString()
        {
            return $"{Email}, {Password}";
        }
    }
}