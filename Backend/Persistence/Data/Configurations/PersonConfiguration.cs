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

        builder.ToTable("persons");

        builder.HasIndex(e => e.PersonTypeId, "IX_Persons_PersonTypeId");

        builder.Property(e => e.Id).HasColumnType("int(11)");
        builder.Property(e => e.Email).HasMaxLength(50);
        builder.Property(e => e.FirstName).HasMaxLength(50);
        builder.Property(e => e.LastName1).HasMaxLength(50);
        builder.Property(e => e.LastName2).HasMaxLength(50);
        builder.Property(e => e.Password).HasMaxLength(50);
        builder.Property(e => e.PersonTypeId).HasColumnType("int(11)");

        builder.HasOne(d => d.PersonType).WithMany(p => p.People)
            .HasForeignKey(d => d.PersonTypeId)
            .HasConstraintName("FK_Persons_PersonTypes_PersonTypeId");
    }
}
