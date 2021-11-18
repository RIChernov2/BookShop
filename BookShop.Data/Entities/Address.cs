namespace Data.Entities
{    /// <summary>
     /// Адрес
     /// </summary>
    public class Address
    {
        public long AddressId { get; set; }

        public long UserId { get; set; }
        
        /// <summary>
        /// Имя адреса (дом / работа и т.д.)
        /// </summary>
        public string AddressName { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Область
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Улица
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Дом / корпус
        /// </summary>
        public string Home { get; set; }

        /// <summary>
        /// Квартира / офис
        /// </summary>
        public string Apartments { get; set; }

        public override string ToString()
        {
            return $"{AddressId}, {UserId}, {Country}, {District}, {City}, {Street}, {Home}, {Apartments}";
        }
    }
}
