using Online_Shop.Models;
using System;

namespace Online_Shop.DTO
{
    public class UserDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }
    }
}
