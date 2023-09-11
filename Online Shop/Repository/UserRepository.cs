using Microsoft.EntityFrameworkCore;
using Online_Shop.DataBaseContext;
using Online_Shop.DTO;
using Online_Shop.InterfaceRepository;
using Online_Shop.Interfaces;
using Online_Shop.Models;
using Online_Shop.Services;
using System;
using System.Collections.Generic;
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

        public async Task<User> GetUserById(int id)
        {
            var user = await dbContext.TableUsers.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await dbContext.TableUsers.ToListAsync();
        }

        public async Task<List<User>> GetAllSellers()
        {
            List<User> sellersForVerification = new List<User>();

            var korisnici = await GetAllUsers();

            foreach (var k in korisnici)
            {
                if (k.Role==Role.SELLER && k.Id>32)
                {
                    sellersForVerification.Add(k);
                }
            }
            return sellersForVerification;

        }
        public async Task<User> GetUserByEmail(string email)
        {
            var user = await dbContext.TableUsers.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }
            return user;
        }
        public async Task<User> GetUserByUserName(string username)
        {
            var user = await dbContext.TableUsers.FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return null;
            }
            return user;
        }


        public async Task<User> UpdateUser(User user)
        {
            dbContext.Update(user);
            await dbContext.SaveChangesAsync();
            return user;
        }
        public async Task RejectVerification(int id)
        {
            var korisnik = await dbContext.TableUsers.FirstOrDefaultAsync(u => u.Id == id);

            if (korisnik != null)
            {
               
                    korisnik.Verified = true;
                    korisnik.StatusApproval = StatusApproval.REJECTED;

                    dbContext.Update(korisnik);
                    await dbContext.SaveChangesAsync();
                
            }
            else
            {
                throw new Exception("Korisnik ne postoji");
            }

        }

        public async Task AcceptVerification(int id)
        {
            var korisnik = await dbContext.TableUsers.FindAsync(id);
            if (korisnik == null)
            {
                throw new Exception("Korisnik ne postoji.");
            }

            if (korisnik.Verified == true)
            {
                throw new Exception("Korisnikova registracija je vec potvrdjena!");
            }


            korisnik.Verified = true;
            korisnik.StatusApproval = StatusApproval.APPROVED;


            dbContext.Update(korisnik);
            await dbContext.SaveChangesAsync();
        }

        //POST
        //dodavanje usera
        public async Task  Register(User user)
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
