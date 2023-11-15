using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class ProductLineConfiguration : IEntityTypeConfiguration<ProductLine>
{
    public void Configure(EntityTypeBuilder<ProductLine> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("product_line");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("cod_product_line");
        builder.Property(e => e.DescriptionHtml)
            .HasColumnType("text")
            .HasColumnName("description_html");
        builder.Property(e => e.DescriptionText)
            .HasColumnType("text")
            .HasColumnName("description_text");
        builder.Property(e => e.Image)
            .HasMaxLength(256)
            .HasColumnName("image");
        builder.Property(e => e.ProductLine1)
            .HasMaxLength(50)
            .HasColumnName("product_line");
    }
}
