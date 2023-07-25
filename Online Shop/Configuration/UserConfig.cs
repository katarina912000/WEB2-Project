using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Shop.Models;

namespace Online_Shop.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u=>u.Id); //setting primary key
            builder.Property(u => u.Id).ValueGeneratedOnAdd(); //Kazem da ce se primarni kljuc
                                                               //automatski generisati prilikom dodavanja,   //redom 1 2 3...
            builder.Property(u => u.Name).HasMaxLength(20).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(20).IsRequired();
            builder.Property(u => u.UserName).HasMaxLength(15).IsRequired();
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(u => u.Email).IsRequired();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.DateOfBirth);
            builder.Property(u => u.Address).IsRequired();
            builder.Property(u => u.Role).HasConversion(new EnumToStringConverter<Role>());
            builder.Property(u => u.StatusApproval).HasConversion(new EnumToStringConverter<StatusApproval>());
            builder.HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);//ako se obrise user brise se i porudzbina

        }
    }
}
