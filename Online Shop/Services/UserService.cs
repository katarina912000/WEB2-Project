using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Online_Shop.DataBaseContext;
using Online_Shop.DTO;
using Online_Shop.InterfaceRepository;
using Online_Shop.Interfaces;
using Online_Shop.Models;
using Online_Shop.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Online_Shop.Services
{
    public class UserService : IUser
    {
        private readonly UserDbContext dbContext;
        private readonly IUserRepo userRepo;
        private readonly IMapper maper;
        private readonly IConfiguration Configuration;
        private string filePath;
        

        public UserService(IConfiguration configuration,UserDbContext dbContext, IUserRepo userRepo, IMapper maper)
        {
            Configuration = configuration;

            this.dbContext = dbContext;
            this.userRepo = userRepo;
            this.maper = maper;
        }
        public async  Task<string> Login(UserLoginDTO user)
        {

            
            User u1 = dbContext.TableUsers.FirstOrDefault(u => u.Email == user.Email);
            if (u1 == null) {

                throw new Exception(string.Format("Korisnik ne postoji! Pogrešan email ili lozinka."));

            }
            string hesiranaLozinka = GetHashedPassword(user.Password);
            if (hesiranaLozinka != u1.Password)
            {
                throw new Exception(string.Format("Neispravna lozinka"));

            }
            var claims = new List<Claim>
            {
                new Claim("UserID",u1.Id.ToString()),
                new Claim(ClaimTypes.Role, u1.Role.ToString()),
                new Claim("StatusApproval", u1.StatusApproval.ToString()),
            };
            var jwtConfigKey = Configuration.GetSection("jwtConfig")["Key"];
            if (string.IsNullOrEmpty(jwtConfigKey))
            {
                throw new Exception("Ključ 'jwtConfig:Key' nema valjanu vrijednost.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigKey));

            var token = new JwtSecurityToken(
              Configuration["jwtConfig:Issuer"],
              Configuration["jwtConfig:Audience"],
              claims,
              expires: DateTime.UtcNow.AddDays(1),
              signingCredentials: new SigningCredentials(
               key, SecurityAlgorithms.HmacSha256Signature));

            var tokenn = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenn;
        }

        public async Task<UserDTO> Register(UserRegistrationDTO user)
        {
            if (AreAllFieldsPopulated(user))
            {
                if (user.Password != user.Password2)
                {
                    throw new Exception("Ne poklapaju se lozinke");

                }
                else if (user.Role == Role.SELLER)
                {
                    User u = maper.Map<UserRegistrationDTO, User>(user);
                    u.StatusApproval = StatusApproval.REJECTED;
                    await SaveImage(user.ImagePath);
                    u.ImagePath = filePath;
                    u.Password = GetHashedPassword(user.Password);
                    u.Password2 = GetHashedPassword(user.Password2);
                    await userRepo.Register(u);
                    return maper.Map<UserDTO>(u);

                   
                }
                else
                {
                    User u = maper.Map<UserRegistrationDTO, User>(user);
                    u.StatusApproval = StatusApproval.APPROVED;
                    await SaveImage(user.ImagePath);
                    u.ImagePath = filePath;
                    u.Password = GetHashedPassword(user.Password);
                    u.Password2 = GetHashedPassword(user.Password2);
                    await userRepo.Register(u);
                    return maper.Map<UserDTO>(u);
                }
            }
            else
            {
                throw new Exception("Moraju sva polja biti popunjena <3");
            }
            
        }

        //metoda za verifikaciju naloga
        public static bool Verification()
        {
            return true;
        }


        //metoda da se vidi da li su sva polja popunjena
        public static bool AreAllFieldsPopulated(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);

                if (value == null || (value is string && string.IsNullOrWhiteSpace((string)value)))
                {
                    return false;
                }
            }

            return true;
        }
        //metoda za hesiranje lozinki
        public static string GetHashedPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                // Konvertujemo heširanu vrednost u heksadecimalni string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
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
