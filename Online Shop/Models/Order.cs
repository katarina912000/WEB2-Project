using System;
using System.Collections.Generic;

namespace Online_Shop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public DateTime DeliveryTime { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public StatusOrder StatusOrder {get;set;}
        public List<Item> Items { get; set; }
        public Order()
        {
            Items = new List<Item>();
        }

    }
}
