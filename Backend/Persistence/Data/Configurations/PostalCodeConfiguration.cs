using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class PostalCodeConfiguration : IEntityTypeConfiguration<PostalCode>
{
    public void Configure(EntityTypeBuilder<PostalCode> builder)
    {
            builder.ToTable("postalcodes");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);
;
            builder.Property(e => e.PostalCode1).HasMaxLength(50).IsRequired();

            builder.HasOne(d => d.City)
                .WithMany(p => p.Postalcodes)
                .HasForeignKey(d => d.CityId);
    }
}
