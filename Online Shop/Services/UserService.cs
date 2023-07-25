using AutoMapper;
using Online_Shop.DataBaseContext;
using Online_Shop.DTO;
using Online_Shop.InterfaceRepository;
using Online_Shop.Interfaces;
using Online_Shop.Models;
using Online_Shop.Repository;
using System.Threading.Tasks;

namespace Online_Shop.Services
{
    public class UserService : IUser
    {
        private readonly UserDbContext dbContext;
        private readonly IUserRepo userRepo;
        private readonly IMapper maper;

        public UserService(UserDbContext dbContext, IUserRepo userRepo, IMapper maper)
        {
            this.dbContext = dbContext;
            this.userRepo = userRepo;
            this.maper = maper;
        }

        
        public async Task<UserDTO> Register(UserRegistrationDTO user)
        {
            User u = maper.Map<UserRegistrationDTO,User>(user);
            u.StatusApproval = StatusApproval.APPROVED;
            u.DateOfBirth = new System.DateTime();
            u.Picture = new byte[3];
            await userRepo.Register(u);
            return maper.Map<UserDTO>(u);//dobra je praksa vratiti nazad kreirani objekat
        }
    }
}
