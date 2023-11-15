using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("city");

        builder.HasIndex(e => e.StateId, "state_id");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("city_id");
        builder.Property(e => e.CityName)
            .HasMaxLength(50)
            .HasColumnName("city_name");
        builder.Property(e => e.StateId)
            .HasColumnType("int(11)")
            .HasColumnName("state_id");

        builder.HasOne(d => d.State).WithMany(p => p.Cities)
            .HasForeignKey(d => d.StateId)
            .HasConstraintName("city_ibfk_1");
    }
}
