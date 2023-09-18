using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Online_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity  { get; set; }
        public string Picture { get; set; }
        public List<Item> Items { get; set; }
        private int korisnikID;
        private User korisnik;

        public Product()
        {
            Items= new List<Item>();
        }
        public int KorisnikID { get => korisnikID; set => korisnikID = value; }
        public User Korisnik { get => korisnik; set => korisnik = value; }
    }
}
