using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class PostalCodeConfiguration : IEntityTypeConfiguration<PostalCode>
{
    public void Configure(EntityTypeBuilder<PostalCode> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("postal_code");

        builder.HasIndex(e => e.CityId, "city_id");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("postal_code_id");
        builder.Property(e => e.CityId)
            .HasColumnType("int(11)")
            .HasColumnName("city_id");
        builder.Property(e => e.PostalCode1)
            .HasMaxLength(10)
            .HasColumnName("postal_code");

        builder.HasOne(d => d.City).WithMany(p => p.PostalCodes)
            .HasForeignKey(d => d.CityId)
            .HasConstraintName("postal_code_ibfk_1");
    }
}
