using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Online_Shop.DataBaseContext;
using Online_Shop.DTO;
using Online_Shop.InterfaceRepository;
using Online_Shop.Interfaces;
using Online_Shop.Models;
using Online_Shop.Repository;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Online_Shop.Services
{
    public class UserService : IUser
    {
        private readonly UserDbContext dbContext;
        private readonly IUserRepo userRepo;
        private readonly IMapper maper;
        private string filePath;

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
            
            await SaveImage(user.ImagePath);
            //u.ImagePath = filePath;
            await userRepo.Register(u);
            return maper.Map<UserDTO>(u);//dobra je praksa vratiti nazad kreirani objekat
        }

        //metoda za cuvanje foto na serveru
        public async Task<string> SaveImage(IFormFile image)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
             filePath = Path.Combine("C:\\Users\\dabet\\Desktop\\FAKS\\WEB2\\WEB2 Projekat\\Photos", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return uniqueFileName; // Vratite ime datoteke kako biste ga mogli spremiti u bazu
        }
        //metoda za konvertovanje u bajte slike
        public byte[] ConvertImageToByteArray(IFormFile image)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Kopiranje podataka iz IFormFile u MemoryStream
                image.CopyTo(memoryStream);

                // Vraćanje bajtovskog niza iz MemoryStream-a
                return memoryStream.ToArray();
            }
        }
    }
}
