using ECommercePaymentIntegration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePaymentIntegration.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> b)
        {
            b.ToTable("Orders");

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();

            b.Property(x => x.OrderStatus)
                .IsRequired();

            b.Property(x => x.CreatedAt)
                .IsRequired();

            b.Property(x => x.Currency).HasMaxLength(3);
            b.Property(x => x.UserId).HasMaxLength(120);


            b.HasIndex(x=>x.UserId);

            b.HasMany(x => x.OrderItems)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            b.Navigation(x => x.OrderItems)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
