using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class StatusConfiguration : IEntityTypeConfiguration<Status>
{
    public void Configure(EntityTypeBuilder<Status> builder)
    {
        builder.ToTable("status");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id);
;
        builder.Property(e => e.NameStatus).HasMaxLength(50).IsRequired();
    }
}
