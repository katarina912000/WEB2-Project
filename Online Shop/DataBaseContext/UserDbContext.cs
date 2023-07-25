using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Online_Shop.Models;



namespace Online_Shop.DataBaseContext
{
    public class UserDbContext : DbContext
    {
        public DbSet<Order> TableOrders { get; set; }
        public DbSet<User> TableUsers { get; set; }
        public DbSet<Product> TableProducts { get; set; }
        public DbSet<Item> TableItems { get; set; }

        public UserDbContext(DbContextOptions options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Kazemo mu da pronadje sve konfiguracije u Assembliju i da ih primeni nad bazom
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
        }
    }
}
