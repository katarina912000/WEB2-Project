using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Shop.Models;

namespace Online_Shop.Configuration
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).ValueGeneratedOnAdd();
            builder.Property(o => o.DeliveryTime).IsRequired();
            builder.Property(o => o.Price).IsRequired();
            builder.Property(o => o.Comment);
            builder.Property(o => o.Address).IsRequired();
            builder.Property(o => o.StatusOrder).HasConversion(new EnumToStringConverter<StatusOrder>());
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(o=>o.Items)
                .WithOne(i=>i.Order)
                .HasForeignKey(i=>i.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
