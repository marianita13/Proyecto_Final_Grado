using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("states");

        builder.HasIndex(e => e.CountryId, "IX_States_CountryId");

        builder.Property(e => e.Id).HasColumnType("int(11)");
        builder.Property(e => e.CountryId).HasColumnType("int(11)");
        builder.Property(e => e.StateName).HasMaxLength(50);

        builder.HasOne(d => d.Country).WithMany(p => p.States)
            .HasForeignKey(d => d.CountryId)
            .HasConstraintName("FK_States_Countries_CountryId");
    }
}
