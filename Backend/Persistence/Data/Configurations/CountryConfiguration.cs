using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("country");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("country_id");
        builder.Property(e => e.CountryName)
            .HasMaxLength(50)
            .HasColumnName("country_name");
    }
}
