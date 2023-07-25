using Online_Shop.DTO;
using Online_Shop.Models;
using System.Threading.Tasks;

namespace Online_Shop.InterfaceRepository
{
    public interface IUserRepo
    { 
        //repo rukuje slojem podataka on moze samo sa modelima podataka da rukuje,ne sme dto
        //User Register(User user);

        Task Register(User user);
        //ovde treba da ide user, umesto dto, i da se to namapira posle
    }
}
