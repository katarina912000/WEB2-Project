using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Online_Shop.Models;

namespace Online_Shop.Configuration
{
    public class ItemConfig : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(i => new {i.OrderId,i.ProductId});
            builder.Property(i=>i.Quantity).IsRequired();
            builder.HasOne(i=>i.Order)
                .WithMany(o=>o.Items)
                .HasForeignKey(i=>i.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i=>i.Product)
                .WithMany(p=>p.Items)
                .HasForeignKey(i=>i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
