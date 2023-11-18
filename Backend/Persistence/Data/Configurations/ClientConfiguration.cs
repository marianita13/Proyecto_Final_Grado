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

        builder.ToTable("client");

        builder.HasIndex(e => e.CodEmployee, "cod_employee");

        builder.HasIndex(e => e.PersonId, "person_id");

        builder.HasIndex(e => e.PostalCodeId, "postal_code_id");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("client_code");
        builder.Property(e => e.ClientName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("client_name");
        builder.Property(e => e.CodEmployee)
            .HasColumnType("int(11)")
            .HasColumnName("cod_employee");
        builder.Property(e => e.CreditLimit)
            .HasPrecision(15, 2)
            .HasColumnName("credit_limit");
        builder.Property(e => e.Fax)
            .IsRequired()
            .HasMaxLength(15)
            .HasColumnName("fax");
        builder.Property(e => e.LineAddress)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("line_address");
        builder.Property(e => e.LineAddress2)
            .HasMaxLength(50)
            .HasColumnName("line_address2");
        builder.Property(e => e.PersonId)
            .HasColumnType("int(11)")
            .HasColumnName("person_id");
        builder.Property(e => e.Phone)
            .IsRequired()
            .HasMaxLength(15)
            .HasColumnName("phone");
        builder.Property(e => e.PostalCodeId)
            .HasColumnType("int(11)")
            .HasColumnName("postal_code_id");

        builder.HasOne(d => d.CodEmployeeNavigation).WithMany(p => p.Clients)
            .HasForeignKey(d => d.CodEmployee)
            .HasConstraintName("client_ibfk_3");

        builder.HasOne(d => d.Person).WithMany(p => p.Clients)
            .HasForeignKey(d => d.PersonId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("client_ibfk_1");

        builder.HasOne(d => d.PostalCode).WithMany(p => p.Clients)
            .HasForeignKey(d => d.PostalCodeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("client_ibfk_2");
    }
}
