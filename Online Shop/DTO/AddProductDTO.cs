using Microsoft.AspNetCore.Http;

namespace Online_Shop.DTO
{
    public class AddProductDTO
    {
       
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public IFormFile Picture { get; set; }
    }
}
