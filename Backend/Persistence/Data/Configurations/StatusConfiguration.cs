using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("status");

        builder.Property(e => e.Id)
            .HasColumnType("int(11)")
            .HasColumnName("cod_status");
        builder.Property(e => e.NameStatus)
            .HasMaxLength(20)
            .HasColumnName("name_status");
    }
}
