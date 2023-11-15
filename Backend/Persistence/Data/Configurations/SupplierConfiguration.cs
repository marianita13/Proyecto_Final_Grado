using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("supplier");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("supplier_id");
        builder.Property(e => e.Fax)
            .HasMaxLength(15)
            .HasColumnName("fax");
        builder.Property(e => e.Name)
            .HasMaxLength(30)
            .HasColumnName("name");
        builder.Property(e => e.Phone)
            .HasMaxLength(15)
            .HasColumnName("phone");
    }
}
