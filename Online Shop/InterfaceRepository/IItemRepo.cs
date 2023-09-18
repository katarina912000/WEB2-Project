using Online_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shop.InterfaceRepository
{
    public interface IItemRepo
    {
        Task<List<Item>> DobaviStavkePorudzbine(int idPorudzbine);

    }
}
