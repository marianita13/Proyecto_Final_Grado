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

        builder.ToTable("productlines");

        builder.Property(e => e.Id).HasColumnType("int(11)");
        builder.Property(e => e.DescriptionHtml).HasMaxLength(50);
        builder.Property(e => e.DescriptionText).HasMaxLength(50);
        builder.Property(e => e.Image).HasMaxLength(50);
        builder.Property(e => e.ProductLine1).HasMaxLength(50);
    }
}
