using Microsoft.EntityFrameworkCore;
using Online_Shop.DataBaseContext;
using Online_Shop.InterfaceRepository;
using Online_Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_Shop.Repository
{
    public class ItemRepository : IItemRepo
    {
        private readonly UserDbContext dbContext;

        public ItemRepository(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Item>> DobaviStavkePorudzbine(int idPorudzbine)
        {
            var porudzbine = await dbContext.TableItems.Where(s => s.OrderId == idPorudzbine).ToListAsync();
            return porudzbine;
        }
    }
}
