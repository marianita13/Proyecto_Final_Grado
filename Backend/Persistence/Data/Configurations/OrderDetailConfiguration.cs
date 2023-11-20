using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable("orderdetails");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.LineNumber).HasColumnType("smallint(6)");
        builder.Property(e => e.Quantity).HasColumnType("int(11)").IsRequired();
        builder.Property(e => e.UnitPrice).HasColumnType("int(11)").IsRequired();

        builder.HasOne(d => d.Order)
            .WithMany(p => p.Orderdetails)
            .HasForeignKey(d => d.OrderId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.Product)
            .WithMany(p => p.Orderdetails)
            .HasForeignKey(d => d.ProductCode)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
