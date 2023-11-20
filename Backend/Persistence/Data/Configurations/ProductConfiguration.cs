using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {;
        builder.ToTable("products");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.Description).HasMaxLength(500000);
        builder.Property(e => e.Dimensions).HasMaxLength(500000);
        builder.Property(e => e.SellingPrice).HasMaxLength(500000);
        builder.Property(e => e.SupplierPrice).HasMaxLength(500000);
        builder.Property(e => e.Name).HasMaxLength(500000).IsRequired();
        builder.Property(e => e.StockQuantity).HasColumnType("smallint(6)").IsRequired();

        builder.HasOne(d => d.Supplier)
            .WithMany(p => p.Products)
            .HasForeignKey(d => d.IdSupplier)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(d => d.ProductLineNavigation)
            .WithMany(p => p.Products)
            .HasForeignKey(d => d.ProductLine)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
