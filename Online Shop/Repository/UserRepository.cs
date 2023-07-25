using Online_Shop.DataBaseContext;
using Online_Shop.DTO;
using Online_Shop.InterfaceRepository;
using Online_Shop.Models;
using Online_Shop.Services;
using System.Threading.Tasks;

namespace Online_Shop.Repository
{
    public class UserRepository : IUserRepo
    {
        private readonly UserDbContext dbContext;
        public UserRepository(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        

        public  async Task  Register(User user)
        {
             dbContext.TableUsers.Add(user);
             dbContext.SaveChanges();
        }

            
    }
}
