using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations;


public class PostalCodeConfiguration : IEntityTypeConfiguration<PostalCode>
{
    public void Configure(EntityTypeBuilder<PostalCode> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("postalcodes");

            builder.HasIndex(e => e.CityId, "IX_PostalCodes_CityId");

            builder.Property(e => e.Id).HasColumnType("int(11)");
            builder.Property(e => e.CityId).HasColumnType("int(11)");
            builder.Property(e => e.PostalCode1).HasMaxLength(50);

            builder.HasOne(d => d.City).WithMany(p => p.Postalcodes)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_PostalCodes_Cities_CityId");
    }
}
