using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class City : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        entity.HasKey(e => e.CityId).HasName("PRIMARY");

        entity.ToTable("city");

        entity.HasIndex(e => e.StateId, "state_id");

        entity.Property(e => e.CityId)
            .HasColumnType("int(11)")
            .HasColumnName("city_id");
        entity.Property(e => e.CityName)
            .HasMaxLength(50)
            .HasColumnName("city_name");
        entity.Property(e => e.StateId)
            .HasColumnType("int(11)")
            .HasColumnName("state_id");

        entity.HasOne(d => d.State).WithMany(p => p.Cities)
            .HasForeignKey(d => d.StateId)
            .HasConstraintName("city_ibfk_1");
    }
}
