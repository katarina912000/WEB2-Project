using Online_Shop.DTO;
using Online_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Online_Shop.Interfaces
{
    public interface IUser
    {
        Task<string> GoogleLogovanje(string token);

        Task<UserDTO> Register(UserRegistrationDTO user);
        Task<string> Login(UserLoginDTO user);//vracam token

        Task<User> GetUserById(int id);
        Task<UserDTO> UpdateUser(int id, UserUpdateDTO user);//vratice i rolu i id
        Task<List<User>> GetAllSeller();
        Task<string> SendMail(string email, string verifikacija);
    }
}
