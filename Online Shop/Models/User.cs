﻿using System;
using System.Collections.Generic;

namespace Online_Shop.Models
{
    
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public StatusApproval StatusApproval { get; set; }

        public List<Order> Orders { get; set; }

        //dodato
        public string ImagePath { get; set; }
        public string Password2 { get; set; }
        public bool Verified { get; set; }
        public bool PasswordHashedOK { get; set; }
       

        public User()
        {
            Orders = new List<Order>();
        }








    }
}
