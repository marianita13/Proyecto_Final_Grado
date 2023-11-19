using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class StateConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder.ToTable("states");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.StateName).HasMaxLength(50).IsRequired();

        builder.HasOne(d => d.Country)
            .WithMany(p => p.States)
            .HasForeignKey(d => d.CountryId);
    }
}
