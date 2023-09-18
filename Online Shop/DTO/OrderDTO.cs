using System.Collections.Generic;

namespace Online_Shop.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        public List<ItemDTO> Items { get; set; }
    }
}
