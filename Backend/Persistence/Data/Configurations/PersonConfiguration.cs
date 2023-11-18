using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("person");

        builder.HasIndex(e => e.PersonTypeId, "person_type_id");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("person_id");
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("email");
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("first_name");
        builder.Property(e => e.LastName1)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("last_name1");
        builder.Property(e => e.LastName2)
            .HasMaxLength(50)
            .HasColumnName("last_name2");
        builder.Property(e => e.PersonTypeId)
            .HasColumnType("int(11)")
            .HasColumnName("person_type_id");

        builder.HasOne(d => d.PersonType).WithMany(p => p.People)
            .HasForeignKey(d => d.PersonTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("person_ibfk_1");
    }
}
