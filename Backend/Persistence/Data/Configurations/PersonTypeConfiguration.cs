using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class PersonTypeConfiguration : IEntityTypeConfiguration<PersonType>
{
    public void Configure(EntityTypeBuilder<PersonType> builder)
    {
        builder.HasKey(e => e.TypeId).HasName("PRIMARY");

        builder.ToTable("person_type");

        builder.Property(e => e.TypeId)
            .HasColumnType("int(11)")
            .HasColumnName("type_id");
        builder.Property(e => e.TypeName)
            .HasMaxLength(50)
            .HasColumnName("type_name");
    }
}
