using Online_Shop.DTO;
using Online_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shop.InterfaceRepository
{
    public interface IUserRepo
    { 
        //repo rukuje slojem podataka on moze samo sa modelima podataka da rukuje,ne sme dto
        //User Register(User user);

        Task Register(User user);
        
        Task<User> GetUserById(int id);
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUserName(string username);

        Task<List<User>> GetAllSellers();
        Task<List<User>> GetAllUsers();
        Task AcceptVerification(int id);
        Task RejectVerification(int id);

        Task<User> UpdateUser(User user);
    }
}
