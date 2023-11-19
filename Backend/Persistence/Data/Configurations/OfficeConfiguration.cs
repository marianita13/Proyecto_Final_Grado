using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.ToTable("offices");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);;

        builder.Property(e => e.AddressLine1).HasMaxLength(50).IsRequired();
        builder.Property(e => e.AddressLine2).HasMaxLength(50);
        builder.Property(e => e.Phone).HasMaxLength(50).IsRequired();

        builder.HasOne(d => d.PostalCode)
            .WithMany(p => p.Offices)
            .HasForeignKey(d => d.PostalCodeId);
    }
}
