using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.Comments).HasMaxLength(500);
        builder.Property(e => e.OrderDate).HasColumnType("Date");
        builder.Property(e => e.ExpectedDate).HasColumnType("Date");
        builder.Property(e => e.DeliveryDate).HasColumnType("Date");

        builder.HasOne(d => d.ClientCodeNavigation)
            .WithMany(p => p.Orders)
            .HasForeignKey(d => d.ClientCode)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.StatusCodeNavigation)
            .WithMany(p => p.Orders)
            .HasForeignKey(d => d.StatusCode)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
