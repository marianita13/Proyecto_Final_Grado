using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("clients");

        builder.HasIndex(e => e.CodEmployee, "IX_Clients_CodEmployee");

        builder.HasIndex(e => e.PersonId, "IX_Clients_PersonId");

        builder.HasIndex(e => e.PostalCodeId, "IX_Clients_PostalCodeId");

        builder.Property(e => e.Id).HasColumnType("int(11)");
        builder.Property(e => e.ClientName).HasMaxLength(50);
        builder.Property(e => e.CodEmployee).HasColumnType("int(11)");
        builder.Property(e => e.Fax).HasMaxLength(50);
        builder.Property(e => e.LineAddress).HasMaxLength(100);
        builder.Property(e => e.LineAddress2).HasMaxLength(100);
        builder.Property(e => e.PersonId).HasColumnType("int(11)");
        builder.Property(e => e.Phone).HasMaxLength(50);
        builder.Property(e => e.PostalCodeId).HasColumnType("int(11)");

        builder.HasOne(d => d.CodEmployeeNavigation).WithMany(p => p.Clients)
            .HasForeignKey(d => d.CodEmployee)
            .HasConstraintName("FK_Clients_Employees_CodEmployee");

        builder.HasOne(d => d.Person).WithMany(p => p.Clients)
            .HasForeignKey(d => d.PersonId)
            .HasConstraintName("FK_Clients_Persons_PersonId");

        builder.HasOne(d => d.PostalCode).WithMany(p => p.Clients)
            .HasForeignKey(d => d.PostalCodeId)
            .HasConstraintName("FK_Clients_PostalCodes_PostalCodeId");
    }
}
