using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("products");

        builder.HasIndex(e => e.ProductLine, "FK_Products_ProductLines_ProductLine");

        builder.HasIndex(e => e.IdSupplier, "FK_Products_Suppliers_IdSupplier");

        builder.Property(e => e.Description).HasMaxLength(50);
        builder.Property(e => e.Dimensions).HasMaxLength(50);
        builder.Property(e => e.IdSupplier).HasColumnType("int(11)");
        builder.Property(e => e.Name).HasMaxLength(50);
        builder.Property(e => e.ProductLine).HasColumnType("int(11)");
        builder.Property(e => e.StockQuantity).HasColumnType("smallint(6)");

        builder.HasOne(d => d.IdSupplierNavigation).WithMany(p => p.Products)
            .HasForeignKey(d => d.IdSupplier)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Products_Suppliers_IdSupplier");

        builder.HasOne(d => d.ProductLineNavigation).WithMany(p => p.Products)
            .HasForeignKey(d => d.ProductLine)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Products_ProductLines_ProductLine");
    }
}
