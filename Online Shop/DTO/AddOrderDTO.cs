using System.Collections.Generic;

namespace Online_Shop.DTO
{
    public class AddOrderDTO
    {
        public string Address { get; set; }
        public List<AddItemDTO> Items { get; set; } = null;
        public string Comment { get; set; }
    }
}
