using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.Id).HasColumnType("int(11)");
        builder.Property(p => p.CityName).IsRequired().HasMaxLength(50);
        builder.Property(e => e.StateId).HasColumnType("int(11)");

        builder.HasOne(d => d.State)
        .WithMany(p => p.Cities)
        .HasForeignKey(d => d.StateId);
    }
}

