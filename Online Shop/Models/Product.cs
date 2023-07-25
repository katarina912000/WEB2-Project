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
        public byte[] Picture { get; set; }
        public List<Item> Items { get; set; }

        public Product()
        {
            Items= new List<Item>();
        }
    }
}
