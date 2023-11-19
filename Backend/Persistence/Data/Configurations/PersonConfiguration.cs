using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);

        builder.Property(e => e.Email).HasMaxLength(50);
        builder.Property(e => e.FirstName).HasMaxLength(50);
        builder.Property(e => e.LastName1).HasMaxLength(50);
        builder.Property(e => e.LastName2).HasMaxLength(50);
        builder.Property(e => e.PersonTypeId).HasColumnType("int(11)");

        builder.HasOne(d => d.PersonType)
            .WithMany(p => p.People)
            .HasForeignKey(d => d.PersonTypeId);
    }
}
