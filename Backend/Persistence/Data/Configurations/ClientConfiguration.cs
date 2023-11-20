using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable("clients");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.ClientName).HasMaxLength(50);
        builder.Property(e => e.Fax).HasMaxLength(50);
        builder.Property(e => e.LineAddress).HasMaxLength(100);
        builder.Property(e => e.LineAddress2).HasMaxLength(100);
        builder.Property(e => e.Phone).HasMaxLength(50);

        builder.HasOne(d => d.CodEmployeeNavigation)
            .WithMany(p => p.Clients)
            .HasForeignKey(d => d.CodEmployee);

        builder.HasOne(d => d.Person).WithMany(p => p.Clients)
            .HasForeignKey(d => d.PersonId);

        builder.HasOne(d => d.PostalCode).WithMany(p => p.Clients)
            .HasForeignKey(d => d.PostalCodeId);
    }
}
