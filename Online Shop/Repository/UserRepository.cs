using Online_Shop.DataBaseContext;
using Online_Shop.DTO;
using Online_Shop.InterfaceRepository;
using Online_Shop.Models;
using Online_Shop.Services;
using System;
using System.Linq;
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

        //POST
        //dodavanje usera
        public  async Task  Register(User user)
        {
            try
            {

                dbContext.TableUsers.Add(user);
                //var listUsers = dbContext.TableUsers.ToList();
                dbContext.SaveChanges();
            }
            catch(Exception e)
            {
                System.Console.WriteLine(e);
            }
            
        }

            
    }
}
