using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(e => e.StateId).HasName("PRIMARY");

        builder.ToTable("state");

        builder.HasIndex(e => e.CountryId, "country_id");

        builder.Property(e => e.StateId)
            .HasColumnType("int(11)")
            .HasColumnName("state_id");
        builder.Property(e => e.CountryId)
            .HasColumnType("int(11)")
            .HasColumnName("country_id");
        builder.Property(e => e.StateName)
            .HasMaxLength(50)
            .HasColumnName("state_name");

        builder.HasOne(d => d.Country).WithMany(p => p.States)
            .HasForeignKey(d => d.CountryId)
            .HasConstraintName("state_ibfk_1");
    }
}
