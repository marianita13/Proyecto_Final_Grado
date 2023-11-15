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

        builder.HasIndex(e => e.PersonId, "person_id");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("client_code");
        builder.Property(e => e.CreditLimit)
            .HasPrecision(15, 2)
            .HasColumnName("credit_limit");
        builder.Property(e => e.Fax)
            .HasMaxLength(15)
            .HasColumnName("fax");
        builder.Property(e => e.PersonId)
            .HasColumnType("int(11)")
            .HasColumnName("person_id");
        builder.Property(e => e.Phone)
            .HasMaxLength(15)
            .HasColumnName("phone");

        builder.HasOne(d => d.Person).WithMany(p => p.Clients)
            .HasForeignKey(d => d.PersonId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("client_ibfk_1");
    }
}
