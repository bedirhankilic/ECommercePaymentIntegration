using ECommercePaymentIntegration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommercePaymentIntegration.Infrastructure.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> b)
        {
            b.ToTable("OrderItems");

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedNever();

            b.Property<Guid>("OrderId")
                .IsRequired();

            b.Property(x => x.ProductId).HasMaxLength(100)
                                         .IsRequired();
            b.HasIndex(c=> c.ProductId);

            b.Property(x => x.Currency).HasMaxLength(3);

            b.Property(x => x.Quantity)
                .IsRequired();

            b.Property(x => x.Category).HasMaxLength(100)
                                         .IsRequired();

        }
    }
}
