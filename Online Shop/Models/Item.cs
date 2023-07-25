namespace Online_Shop.Models
{
    public class Item
    {
        public int Quantity { get; set; }
        
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }


    }
}
