using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(e => new { e.OrderCode, e.ProductCode })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        builder.ToTable("order_detail");

        builder.HasIndex(e => e.ProductCode, "product_code");

        builder.Property(e => e.OrderCode)
            .HasColumnType("int(11)")
            .HasColumnName("order_code");
        builder.Property(e => e.ProductCode)
            .HasMaxLength(15)
            .HasColumnName("product_code");
        builder.Property(e => e.LineNumber)
            .HasColumnType("smallint(6)")
            .HasColumnName("line_number");
        builder.Property(e => e.Quantity)
            .HasColumnType("int(11)")
            .HasColumnName("quantity");
        builder.Property(e => e.UnitPrice)
            .HasPrecision(15, 2)
            .HasColumnName("unit_price");

        builder.HasOne(d => d.OrderCodeNavigation).WithMany(p => p.OrderDetails)
            .HasForeignKey(d => d.OrderCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("order_detail_ibfk_1");

        builder.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.OrderDetails)
            .HasForeignKey(d => d.ProductCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("order_detail_ibfk_2");
    }
}
