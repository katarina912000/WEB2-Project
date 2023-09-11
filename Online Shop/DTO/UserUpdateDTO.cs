using Microsoft.AspNetCore.Http;
using Online_Shop.Models;
using System;

namespace Online_Shop.DTO
{
    public class UserUpdateDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        

        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        
        public IFormFile? ImagePath { get; set; }
    }
}
