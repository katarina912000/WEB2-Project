using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Online_Shop.DataBaseContext;
using Online_Shop.DTO;
using Online_Shop.InterfaceRepository;
using Online_Shop.Interfaces;
using Online_Shop.Models;
using System;
using Google.Apis.Auth;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;


namespace Online_Shop.Services
{
    public class UserService : IUser
    {
        private readonly UserDbContext dbContext;
        private readonly IUserRepo userRepo;
        private readonly IMapper maper;
        private readonly IConfigurationSection googleConfig;

        private readonly IConfiguration Configuration;
        private string filePath;

       
        
        public UserService(IConfiguration configuration,UserDbContext dbContext, IUserRepo userRepo, IMapper maper)
        {
            Configuration = configuration;
            googleConfig = configuration.GetSection("Webclient1");

            this.dbContext = dbContext;
            this.userRepo = userRepo;
            this.maper = maper;
        }
        private async Task<GoogleDTO> VerifikacijGoogleTokena(string loginToken)
        {
            
            var validacija = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>() { googleConfig.Value }
            };

            var googleInfoKorisnika = await GoogleJsonWebSignature.ValidateAsync(loginToken, validacija);

            GoogleDTO googleKorisnik = new()
            {
                Email = googleInfoKorisnika.Email,
               UserName = googleInfoKorisnika.Email.Split("@")[0],
                Name = googleInfoKorisnika.GivenName,
                LastName = googleInfoKorisnika.FamilyName

            };

            return googleKorisnik;

           
        }
        public async Task<string> GoogleLogovanje(string token)
        {
            GoogleDTO googleKorisnik = await VerifikacijGoogleTokena(token);


            if (googleKorisnik == null)
            {

                throw new Exception("Nepostojeci ili neispravan google token korisnika");

            }

            List<User> sviKorisnici = await userRepo.GetAllUsers();


            User korisnik = sviKorisnici.Find(k => k.Email.Equals(googleKorisnik.Email));

            if (korisnik == null)
            {
                korisnik = new User()
                {
                    Name = googleKorisnik.Name,
                    LastName = googleKorisnik.LastName,
                    UserName = googleKorisnik.UserName,
                    Email = googleKorisnik.Email,
                    Password = "",
                    Password2 = "",
                    Address = "",
                    DateOfBirth = DateTime.Now,
                    Role = Role.CUSTOMER,
                    StatusApproval = StatusApproval.APPROVED,
                    ImagePath = "",
                };


                dbContext.TableUsers.Add(korisnik);
                await dbContext.SaveChangesAsync();
            }

            var claims = new List<Claim>
            {
                new Claim("UserID", korisnik.Id.ToString()),
                new Claim(ClaimTypes.Role, korisnik.Role.ToString()),
                new Claim("StatusApproval", korisnik.StatusApproval.ToString()),
            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwtConfig:Key"]));

            var token1 = new JwtSecurityToken(
              Configuration["jwtConfig:Issuer"],
              Configuration["jwtConfig:Audience"],
              claims,
              expires: DateTime.UtcNow.AddDays(1),
              signingCredentials: new SigningCredentials(
              key, SecurityAlgorithms.HmacSha256Signature));


            var tokenn = new JwtSecurityTokenHandler().WriteToken(token1);

            return tokenn;
        }


        public async Task<User> GetUserById(int id)
        {
            var user = await userRepo.GetUserById(id);
            
            if (user == null)
            {
                throw new Exception("Korisnik  ne postoji u bazi");
            }


            return user;
        }

        public async Task<UserDTO> UpdateUser(int id, UserUpdateDTO user)
        {
            var u = await userRepo.GetUserById(id);
            if (u == null)
            {
                throw new Exception("Korisnik ne postoji u bazi");
            }

            // Provjerite da li su polja promijenjena i ažurirajte ih ako jeste
            bool isUpdated = false;

            if (user.Name != u.Name)
            {
                u.Name = user.Name;
                isUpdated = true;
            }

            if (user.LastName != u.LastName)
            {
                u.LastName = user.LastName;
                isUpdated = true;
            }

            if (user.UserName != u.UserName)
            {
                // Proverite da li postoji korisnik sa datim korisničkim imenom
                var existingUserWithUsername = await userRepo.GetUserByUserName(user.UserName);
                if (existingUserWithUsername != null && existingUserWithUsername.Id != id)
                {
                    throw new Exception("Korisnik sa datim korisničkim imenom već postoji u bazi");
                }

                u.UserName = user.UserName;
                isUpdated = true;
            }

            if (user.Email != u.Email)
            {
                // Proverite da li postoji korisnik sa datom email adresom
                var existingUserWithEmail = await userRepo.GetUserByEmail(user.Email);
                if (existingUserWithEmail != null && existingUserWithEmail.Id != id)
                {
                    throw new Exception("Korisnik sa datim emailom već postoji u bazi");
                }

                u.Email = user.Email;
                isUpdated = true;
            }

            if ( user.DateOfBirth != u.DateOfBirth)
            {
                u.DateOfBirth = user.DateOfBirth;
                isUpdated = true;
            }

            if (user.Address != u.Address)
            {
                u.Address = user.Address;
                isUpdated = true;
            }

            if ( user.Password != u.Password)
            {
                u.Password = GetHashedPassword(user.Password);
                isUpdated = true;
            }

            if (user.ImagePath!=null && user.ImagePath.ToString() != u.ImagePath)
            {
                await SaveImage(user.ImagePath);
                u.ImagePath = filePath;
                isUpdated = true;
            }

            if (isUpdated)
            {
                var kor = await userRepo.UpdateUser(u);
                return maper.Map<UserDTO>(kor);
            }
            else
            {
                // Nijedno polje nije promijenjeno, vraćamo postojeći korisniky
                return maper.Map<UserDTO>(u);
            }
        }

        public async Task<string> SendMail(string mail, string verifikacija)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("admweb522@outlook.com"));
            
            
            var existingUserWithEmail =  await userRepo.GetUserByEmail(mail);
            if (verifikacija == "acc")
            {
                await userRepo.AcceptVerification(existingUserWithEmail.Id);
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = "Vas nalog: "+ existingUserWithEmail.UserName+" je prihvacen. Slobodno se ulogujte! :)" };
            }
            else if (verifikacija == "rej")
            {
                await userRepo.RejectVerification(existingUserWithEmail.Id);              
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = "Vas pokusaj " + existingUserWithEmail.UserName+" za registraciju je odbijen, nazalost." };
            }
            else
            {

                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = verifikacija };

            }
            if (!(mail == "dabetickatarina4@gmail.com"))
            {
                mail = "dabetickatarina4@gmail.com";
            }
            email.To.Add(MailboxAddress.Parse(mail));

            email.Subject = "Verifikacija";
        
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp-mail.outlook.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("admweb522@outlook.com", "admwebadm!");
           
            smtp.Send(email);
            smtp.Disconnect(true);
            return "poslatooooo";
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
                    u.Verified = false;
                    string path = await SaveImage(user.ImagePath);
                    u.ImagePath = path;
                    u.Password = GetHashedPassword(user.Password);
                    u.Password2 = GetHashedPassword(user.Password2);
                    //ovde pozveti fju kojoj cemo posalti mejl od ove osobe

                    //ovde odmah pozvati fju koja salje mejl da se zahtev procesira
                   // var sta=SendMail("dabetickatarina4@gmail.com", "Vas zahtev za registraciju je u procesu obrade, hvala na strpljenju.");
                    
                    await userRepo.Register(u);
                    return maper.Map<UserDTO>(u);

                   
                }
                else
                {
                    User u = maper.Map<UserRegistrationDTO, User>(user);
                    u.StatusApproval = StatusApproval.APPROVED;
                    string path = await SaveImage(user.ImagePath);
                    u.ImagePath = path;
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

        public async Task<List<User>> GetAllSeller()
        {
            return await userRepo.GetAllSellers();
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
        //metoda za cuvanje foto
        public async Task<string> SaveImage(IFormFile image)
        {
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
             filePath = Path.Combine("C:\\Users\\dabet\\Desktop\\FAKS\\WEB2\\git2vscode\\WEB2-Project\\uploads", uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            byte[] imageBytes = File.ReadAllBytes(filePath);
            string base64Image = Convert.ToBase64String(imageBytes);
            //dodato da se u bazu sacuva url a ne stvarna fizicka adresa
            var imageUrl = "https://localhost:44312/uploads/" + uniqueFileName; // URL za sliku
            string img64= "data:image/jpeg;base64," + base64Image;

            return img64; // Vratite ime datoteke kako biste ga mogli spremiti u bazu
        }
        
    }
}
