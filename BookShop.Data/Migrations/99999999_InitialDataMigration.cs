using FluentMigrator;


namespace Data.Migrations
{
    [Profile("FirstStart")]
    public class InitialDataMigration : Migration {
        public override void Up()
        {
            Insert.IntoTable("users")
                .Row(new { name = "Super", surname = "Admin", age = 30, email = "admin", password = "admin" })
                .Row(new { name = "Loren", surname = "Wash", age = 17, email = "LorenWash@gmail.com", password = "Wash123" })
                .Row(new { name = "Alysha", surname = "Holland", age = 38, email = "AlyshaHolland@gmail.com", password = "Holland123" })
                .Row(new { name = "Michelle", surname = "Young", age = 55, email = "MichelleYoung@gmail.com", password = "Young123" })
                .Row(new { name = "Harriett", surname = "Fosse", age = 32, email = "HarriettFosse@gmail.com", password = "Fosse123" })
                .Row(new { name = "Jade", surname = "Christopherson", age = 27, email = "JadeChristopherson@gmail.com", password = "Christopherson123" });

            Insert.IntoTable("addresses")
                .Row(new { address_name = "Home", country = "Russia", district = "", city = "Moscow", street = "Semenovskaya st.", home = "22", apartments = "13", user_id = 1 })
                .Row(new { address_name = "Work", country = "Russia", district = "", city = "St. Petersburg", street = "B. Ordynka st.", home = "9", apartments = "3", user_id = 2 })
                .Row(new { address_name = "MyAddress", country = "Russia", district = "", city = "Kolomna", street = "Novomoskovskaya st.", home = "33", apartments = "12", user_id = 3 })
                .Row(new { address_name = "Friends", country = "USA", district = "", city = "New York", street = "Washington st.", home = "3", apartments = "407", user_id = 4 })
                .Row(new { address_name = "Home", country = "USA", district = "", city = "Salt Lake City", street = "Lomonosov st.", home = "2", apartments = "14", user_id = 5 })
                .Row(new { address_name = "WorkAddress", country = "Russia", district = "", city = "Tver", street = "Serova st.", home = "61", apartments = "35", user_id = 5 });

            Insert.IntoTable("carts")
                .Row(new { user_id = 1 })
                .Row(new { user_id = 2 })
                .Row(new { user_id = 3 })
                .Row(new { user_id = 4 })
                .Row(new { user_id = 5 });

            Insert.IntoTable("cart_products")
                .Row(new { cart_id = 1, book_id = 1, amount = 1 })
                .Row(new { cart_id = 1, book_id = 2, amount = 2 })
                .Row(new { cart_id = 2, book_id = 6, amount = 1 })
                .Row(new { cart_id = 2, book_id = 7, amount = 1 })
                .Row(new { cart_id = 3, book_id = 4, amount = 3 })
                .Row(new { cart_id = 3, book_id = 5, amount = 1 })
                .Row(new { cart_id = 4, book_id = 1, amount = 1 })
                .Row(new { cart_id = 5, book_id = 4, amount = 1 })
                .Row(new { cart_id = 5, book_id = 3, amount = 1 });
        }

        public override void Down()
        {
        }
    }
}
