using ECommercePaymentIntegration.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommercePaymentIntegration.Infrastructure
{
    public sealed class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ECommerce");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ECommerceDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
