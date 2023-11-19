using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.HasKey(e => new { e.Id, e.ProductCode })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        builder.ToTable("orderdetails");

        builder.HasIndex(e => e.Id, "FK_OrderDetails_Orders_Order");

        builder.HasIndex(e => e.ProductCode, "FK_OrderDetails_ProductCode_ProductCode");

        builder.Property(e => e.Id).HasColumnType("int(11)");
        builder.Property(e => e.LineNumber).HasColumnType("smallint(6)");
        builder.Property(e => e.Quantity).HasColumnType("int(11)");

        builder.HasOne(d => d.IdNavigation).WithMany(p => p.Orderdetails)
            .HasForeignKey(d => d.Id)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OrderDetails_Orders_Order");

        builder.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.Orderdetails)
            .HasForeignKey(d => d.ProductCode)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_OrderDetails_ProductCode_ProductCode");
    }
}
