using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Configuration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p=>p.Name).IsRequired().HasMaxLength(25);
            builder.Property(p=>p.Description).IsRequired().HasMaxLength(50);
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();
            builder.HasMany(p=>p.Items)
                .WithOne(i=> i.Product)
                .HasForeignKey(i=>i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
