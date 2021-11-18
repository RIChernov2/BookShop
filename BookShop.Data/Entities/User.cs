using System.Collections.Generic;
using System.Text.Json.Serialization;
using Data.Entities.Interfaces;

namespace Data.Entities
{
    public class User : IPerson
    {
        public long UserId { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Возраст
        /// </summary>
        public byte? Age { get; set; }

        /// <summary>
        /// Электронная почта, она же логин
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [JsonIgnore]
        public string Password { get; set; }

        /// <summary>
        /// JWT токен (не хранится в базе)
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        public List<Address> Addresses { get; set; }

        public User()
        {
            Addresses = new List<Address>();
        }

        public override string ToString()
        {
            return $"{UserId}, {Name}, {Surname}, {Age}, {Email}";
        }
    }
}