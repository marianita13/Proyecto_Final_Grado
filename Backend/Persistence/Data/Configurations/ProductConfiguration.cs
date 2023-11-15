using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.ProductCode).HasName("PRIMARY");

        builder.ToTable("product");

        builder.HasIndex(e => e.IdSupplier, "id_supplier");

        builder.HasIndex(e => e.ProductLine, "product_line");

        builder.Property(e => e.ProductCode)
            .HasMaxLength(15)
            .HasColumnName("product_code");
        builder.Property(e => e.Description)
            .HasColumnType("text")
            .HasColumnName("description");
        builder.Property(e => e.Dimensions)
            .HasMaxLength(25)
            .HasColumnName("dimensions");
        builder.Property(e => e.IdSupplier)
            .HasColumnType("int(11)")
            .HasColumnName("id_supplier");
        builder.Property(e => e.Name)
            .HasMaxLength(70)
            .HasColumnName("name");
        builder.Property(e => e.ProductLine)
            .HasColumnType("int(11)")
            .HasColumnName("product_line");
        builder.Property(e => e.SellingPrice)
            .HasPrecision(15, 2)
            .HasColumnName("selling_price");
        builder.Property(e => e.StockQuantity)
            .HasColumnType("smallint(6)")
            .HasColumnName("stock_quantity");
        builder.Property(e => e.SupplierPrice)
            .HasPrecision(15, 2)
            .HasColumnName("supplier_price");

        builder.HasOne(d => d.IdSupplierNavigation).WithMany(p => p.Products)
            .HasForeignKey(d => d.IdSupplier)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("product_ibfk_1");

        builder.HasOne(d => d.ProductLineNavigation).WithMany(p => p.Products)
            .HasForeignKey(d => d.ProductLine)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("product_ibfk_2");
    }
}
