using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("offices");

        builder.HasIndex(e => e.PostalCodeId, "IX_Offices_PostalCodeId");

        builder.Property(e => e.AddressLine1).HasMaxLength(50);
        builder.Property(e => e.AddressLine2).HasMaxLength(50);
        builder.Property(e => e.Phone).HasMaxLength(50);
        builder.Property(e => e.PostalCodeId).HasColumnType("int(11)");

        builder.HasOne(d => d.PostalCode).WithMany(p => p.Offices)
            .HasForeignKey(d => d.PostalCodeId)
            .HasConstraintName("FK_Offices_PostalCodes_PostalCodeId");
    }
}
