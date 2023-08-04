using Microsoft.AspNetCore.Http;
using Online_Shop.Models;
using System;

namespace Online_Shop.DTO
{
    public class UserRegistrationDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
        public IFormFile? ImagePath { get; set; }
    }
}
