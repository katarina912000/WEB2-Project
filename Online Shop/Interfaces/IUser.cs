using Online_Shop.DTO;
using Online_Shop.Models;
using System.Threading.Tasks;

namespace Online_Shop.Interfaces
{
    public interface IUser
    {
        Task<UserDTO> Register(UserRegistrationDTO user);//task<int> znaci da mi vraca id

    }
}
