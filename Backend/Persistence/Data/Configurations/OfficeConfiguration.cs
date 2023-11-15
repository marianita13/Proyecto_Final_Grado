using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        builder.HasKey(e => e.OfficeCode).HasName("PRIMARY");

        builder.ToTable("office");

        builder.HasIndex(e => e.PostalCodeId, "postal_code_id");

        builder.Property(e => e.OfficeCode)
            .HasMaxLength(10)
            .HasColumnName("office_code");
        builder.Property(e => e.AddressLine1)
            .HasMaxLength(50)
            .HasColumnName("address_line1");
        builder.Property(e => e.AddressLine2)
            .HasMaxLength(50)
            .HasColumnName("address_line2");
        builder.Property(e => e.Phone)
            .HasMaxLength(20)
            .HasColumnName("phone");
        builder.Property(e => e.PostalCodeId)
            .HasColumnType("int(11)")
            .HasColumnName("postal_code_id");

        builder.HasOne(d => d.PostalCode).WithMany(p => p.Offices)
            .HasForeignKey(d => d.PostalCodeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("office_ibfk_1");
    }
}
